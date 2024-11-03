using System;
using System.Collections.Generic;
using System.Linq; // Certifique-se de incluir esta diretiva para usar LINQ
using Microsoft.ML;
using Microsoft.ML.Data; // Para IDataView e outras classes
using SavvyfixAspNet.ML;

namespace Projeto.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MLContext();

            // Caminho para o arquivo de dados
            var dataPath = "datasetIA.csv"; // Ajuste o caminho para o seu arquivo
            var data = context.Data.LoadFromTextFile<AtividadesDataComPorcentagem>(
                dataPath,
                separatorChar: ';',
                hasHeader: true
            );

            float precoBase = 100.0f; // Preço base como float

            // Converter IDataView para IEnumerable
            var dadosComoEnumerable = context.Data.CreateEnumerable<AtividadesDataComPorcentagem>(data, reuseRowObject: false);

            // Criar critérios de porcentagem
            var criterios = new PorcentagemCriterios();

            // Calcular o Resultado para cada item
            var dadosComResultados = dadosComoEnumerable.Select(item =>
            {
                item = PreencherPorcentagens(item, criterios);
                item.PrecoFinal = CalculateResultado(item, precoBase);
                return item;
            }).ToList();

            // Validar e limpar dados
            var dadosLimpos = dadosComResultados
                .Where(item => !float.IsNaN(item.PorcentagemLocalizacao) &&
                               !float.IsNaN(item.PorcentagemHorario) &&
                               !float.IsNaN(item.PorcentagemClima) &&
                               !float.IsNaN(item.PorcentagemProcura) &&
                               !float.IsNaN(item.PorcentagemDemanda))
                .ToList();

            // Criar um novo IDataView a partir da lista com resultados
            var trainingData = context.Data.LoadFromEnumerable(dadosLimpos);

            // Pipeline de treinamento
            var pipeline = context.Transforms.Concatenate("Features", 
                    nameof(AtividadesDataComPorcentagem.PorcentagemLocalizacao), 
                    nameof(AtividadesDataComPorcentagem.PorcentagemHorario),
                    nameof(AtividadesDataComPorcentagem.PorcentagemClima),
                    nameof(AtividadesDataComPorcentagem.PorcentagemProcura),
                    nameof(AtividadesDataComPorcentagem.PorcentagemDemanda))
                .Append(context.Regression.Trainers.Sdca(labelColumnName: nameof(AtividadesDataComPorcentagem.PrecoFinal), maximumNumberOfIterations: 100));

            // Treinar o modelo
            var model = pipeline.Fit(trainingData);

            // Fazer previsões
            var previsoes = model.Transform(trainingData);

            // Exibir resultados
            var resultados = context.Data.CreateEnumerable<ResultadoPrevisao>(previsoes, reuseRowObject: false).ToList();

            foreach (var resultado in resultados)
            {
                Console.WriteLine($"Nome: {resultado.Nome}, Preço Final Previsto: {resultado.PrecoFinal}");
            }
        }

        public static AtividadesDataComPorcentagem PreencherPorcentagens(AtividadesDataComPorcentagem data, PorcentagemCriterios criterios)
        {
            string intervaloHorario = ObterIntervaloHorario(data.Horario);
            int climaInt = (int)data.Clima; 
            int procuraInt = (int)data.Procura;
            string intervaloClima = ObterIntervaloClima(climaInt);
            string intervaloProcura = ObterIntervaloProcura(procuraInt);

            return new AtividadesDataComPorcentagem()
            {
                Nome = data.Nome,
                Produto = data.Produto,
                Localizacao = data.Localizacao,
                Horario = data.Horario,
                Clima = data.Clima,
                Procura = data.Procura,
                Demanda = data.Demanda,
                PorcentagemLocalizacao = criterios.PorcentagemLocalizacao.ContainsKey(data.Localizacao) 
                    ? (float)criterios.PorcentagemLocalizacao[data.Localizacao] 
                    : 0f,
                PorcentagemHorario = criterios.PorcentagemHorario.ContainsKey(intervaloHorario) 
                    ? (float)criterios.PorcentagemHorario[intervaloHorario] 
                    : 0f,
                PorcentagemClima = criterios.PorcentagemClima.ContainsKey(intervaloClima) 
                    ? (float)criterios.PorcentagemClima[intervaloClima] 
                    : 0f,
                PorcentagemProcura = criterios.PorcentagemProcura.ContainsKey(intervaloProcura) 
                    ? (float)criterios.PorcentagemProcura[intervaloProcura] 
                    : 0f,
                PorcentagemDemanda = criterios.PorcentagemDemanda.ContainsKey(data.Demanda) 
                    ? (float)criterios.PorcentagemDemanda[data.Demanda] 
                    : 0f,
                PrecoFinal = 0f // Inicializando como float
            };
        }

        private static string ObterIntervaloHorario(string horario)
        {
            if (string.IsNullOrWhiteSpace(horario) || !horario.Contains(':'))
            {
                return "Desconhecido"; // Ou algum valor padrão que você prefira
            }

            var hora = Convert.ToInt32(horario.Split(':')[0]);
            return $"{hora}h-{hora}h59";
        }

        private static string ObterIntervaloClima(int clima)
        {
            if (clima >= 1 && clima <= 3) return "1º a 3º";
            if (clima >= 4 && clima <= 6) return "4º a 6º";
            if (clima >= 7 && clima <= 9) return "7º a 9º";
            if (clima >= 10 && clima <= 12) return "10º a 12º";
            if (clima >= 13 && clima <= 15) return "13º a 15º";
            if (clima >= 16 && clima <= 18) return "16º a 18º";
            if (clima >= 19 && clima <= 21) return "19º a 21º";
            if (clima >= 22 && clima <= 24) return "22º a 24º";
            if (clima >= 25 && clima <= 27) return "25º a 27º";
            if (clima >= 28 && clima <= 30) return "28º a 30º";
            if (clima >= 31 && clima <= 33) return "31º a 33º";
            if (clima >= 34 && clima <= 36) return "34º a 36º";
            if (clima >= 37 && clima <= 39) return "37º a 39º";
            return "+40º";
        }
        
        private static string ObterIntervaloProcura(int numeroProcura)
        {
            if (numeroProcura == 1) return "1x";
            if (numeroProcura >= 2 && numeroProcura <= 3) return "2 a 3x";
            if (numeroProcura >= 4 && numeroProcura <= 7) return "4 a 7x";
            if (numeroProcura >= 8 && numeroProcura <= 13) return "8 a 13x";
            if (numeroProcura >= 14 && numeroProcura <= 17) return "14 a 17x";
            if (numeroProcura >= 17 && numeroProcura <= 20) return "17 a 20x";
            return "+21x";
        }


        public static float CalculateResultado(AtividadesDataComPorcentagem data, float precoBase)
        {
            float resultado = precoBase;

            // Exibir o preço base
            Console.WriteLine($"Preço Base: {precoBase}");

            // Aplicar as porcentagens
            resultado *= (1 + data.PorcentagemLocalizacao);
            Console.WriteLine($"Após Localização ({data.PorcentagemLocalizacao * 100}%): {resultado}");

            resultado *= (1 + data.PorcentagemHorario);
            Console.WriteLine($"Após Horário ({data.PorcentagemHorario * 100}%): {resultado}");

            resultado *= (1 + data.PorcentagemClima);
            Console.WriteLine($"Após Clima ({data.PorcentagemClima * 100}%): {resultado}");

            resultado *= (1 + data.PorcentagemProcura);
            Console.WriteLine($"Após Procura ({data.PorcentagemProcura * 100}%): {resultado}");

            resultado *= (1 + data.PorcentagemDemanda);
            Console.WriteLine($"Após Demanda ({data.PorcentagemDemanda * 100}%): {resultado}");

            return resultado;
        }

    }

    // Classe para armazenar os resultados da previsão
    public class ResultadoPrevisao
    {
        public string Nome { get; set; }
        public float PrecoFinal { get; set; }
    }
}
