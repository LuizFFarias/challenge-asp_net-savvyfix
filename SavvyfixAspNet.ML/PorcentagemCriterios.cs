using System.Collections.Generic;

namespace SavvyfixAspNet.ML
{
    public class PorcentagemCriterios
    {
        public Dictionary<string, double> PorcentagemLocalizacao { get; } = new Dictionary<string, double>
        {
            { "RS", 0.03 }, { "SC", 0.02 }, { "PR", 0.01 }, { "SP", 0.03 }, { "RJ", 0.02 }, { "ES", 0 },
            { "MG", 0.01 }, { "MT", -0.01 }, { "MS", -0.01 }, { "GO", -0.01 }, { "AM", -0.02 }, { "AC", -0.03 },
            { "RO", -0.02 }, { "RR", -0.02 }, { "AP", -0.02 }, { "PA", 0 }, { "TO", -0.01 }, { "MA", 0.01 },
            { "PI", -0.01 }, { "BA", 0.02 }, { "CE", 0.01 }, { "RN", -0.01 }, { "SE", -0.02 }, { "AL", 0 },
            { "PE", 0.01 }, { "PB", -0.02 }, { "Desconhecido", 0 }
        };

        public Dictionary<string, double> PorcentagemHorario { get; } = new Dictionary<string, double>
        {
            { "00h-00h59", -0.01 }, { "01h-01h59", -0.02 }, { "02h-02h59", -0.03 }, { "03h-03h59", -0.03 },
            { "04h-04h59", -0.02 }, { "05h-05h59", -0.01 }, { "06h-06h59", -0.01 }, { "07h-07h59", 0 },
            { "08h-08h59", 0 }, { "09h-09h59", 0.01 }, { "10h-10h59", 0.01 }, { "11h-11h59", 0.02 },
            { "12h-12h59", 0.03 }, { "13h-13h59", 0.03 }, { "14h-14h59", 0.02 }, { "15h-15h59", 0.01 },
            { "16h-16h59", 0 }, { "17h-17h59", 0 }, { "18h-18h59", 0.01 }, { "19h-19h59", 0.02 },
            { "20h-20h59", 0.03 }, { "21h-21h59", 0.02 }, { "22h-22h59", 0.01 }, { "23h-23h59", 0 }
        };

        public Dictionary<string, double> PorcentagemClima { get; } = new Dictionary<string, double>
        {
            { "1º a 3º", 0.03 }, { "4º a 6º", 0.02 }, { "7º a 9º", 0.02 }, { "10º a 12º", 0.01 }, { "13º a 15º", 0 },
            { "16º a 18º", -0.01 }, { "19º a 21º", -0.02 }, { "22º a 24º", -0.02 }, { "25º a 27º", 0 }, { "28º a 30º", 0.01 },
            { "31º a 33º", 0.01 }, { "34º a 36º", 0.02 }, { "37º a 39º", 0.03 }, { "+40º", 0.04 }
        };

        public Dictionary<string, double> PorcentagemProcura { get; } = new Dictionary<string, double>
        {
            { "1x", 0 }, { "2 a 3x", 0.01 }, { "4 a 7x", 0.03 }, { "8 a 13x", 0 }, { "14 a 17x", -0.02 },
            { "17 a 20x", 0.01 }, { "+21x", 0 }, { "Desconhecido", 0 }
        };

        public Dictionary<string, double> PorcentagemDemanda { get; } = new Dictionary<string, double>
        {
            { "Alta", 0.02 }, { "Média", 0 }, { "Baixa", -0.02 }, { "Desconhecido", 0 }
        };
    }
}
