namespace Atividade_06_Vitrine.Models;

public class Usuario {

    public int Codigo { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }

    public Usuario(int codigo, string nome, string endereco) {
        Codigo = codigo;
        Nome = nome;
        Endereco = endereco;
    }

}
