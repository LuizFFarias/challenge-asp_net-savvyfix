public class AtividadeInputModel
{
    public string Nome { get; set; }
    public string Produto { get; set; }
    public string Localizacao { get; set; }
    public string Horario { get; set; }
    public int Clima { get; set; }
    public int Procura { get; set; }
    public string Demanda { get; set; }
    public float PrecoBase { get; set; } // Preço base pode ser opcional para a previsão

    // Construtor para facilitar a criação do objeto
    public AtividadeInputModel(string nome, string produto, string localizacao, string horario, int clima, int procura, string demanda, float precoBase)
    {
        Nome = nome;
        Produto = produto;
        Localizacao = localizacao;
        Horario = horario;
        Clima = clima;
        Procura = procura;
        Demanda = demanda;
        PrecoBase = precoBase;
    }

    // Construtor padrão sem argumentos
    public AtividadeInputModel() { }
}