﻿using cadastro.Enums;
using System.ComponentModel.DataAnnotations;
using cadastro.Helper;
using System.ComponentModel.DataAnnotations.Schema;


namespace cadastro.Models;

    
public class UsuarioModel

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite o nome do usuário")]
    [MaxLength(255)]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Digite o login do usuário")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Digite o e-mail do usuário")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
    [MaxLength(255)]
    public string Email { get; set; }
    [Required(ErrorMessage = "Informe o perfil do usuário")]
    public PerfilEnum? Perfil { get; set; }
    [Required(ErrorMessage = "Digite a senha do usuário")]
    public string Senha { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public virtual List<ContatoModel> Contatos { get; set; }

    public bool SenhaValida(string senha)
    {
        return Senha == senha.GerarHash();
    }

    public void SetSenhaHash()
    {
        Senha = Senha.GerarHash();
    }

    public void SetNovaSenha(string novaSenha)
    {
        Senha = novaSenha.GerarHash();
    }

    public string GerarNovaSenha()
    {
        string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
        Senha = novaSenha.GerarHash();
        return novaSenha;
    }
}

