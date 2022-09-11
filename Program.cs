using System.Data;
// See https://aka.ms/new-console-template for more information
using alexandre_d3_avaliacao.Models;
using accessing_db.Repositories;
using DotNetEnv;

namespace accessing_db
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();
            Console.WriteLine();
            UserRepository _user = new();

            bool encerrar = false; string option;
            string email; string senha;
            string opcoesdelogin; string nome;

            do
            {
                Console.WriteLine("Escolha uma das opções abaixo:");
                Console.WriteLine("1 - Acessar");
                Console.WriteLine("2 - Adicionar Usuário");
                Console.WriteLine("0 - Sair da aplicação");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Digite seu email:");
                        email = Console.ReadLine();
                        Console.WriteLine("Digite sua senha:");
                        senha = Console.ReadLine();
                        User usuario = _user.ValidateUser(email, senha);

                        if (usuario == null) Console.WriteLine("Senha ou usuário incorreto.");
                        else
                        {
                            Console.WriteLine("Agora você está logado.");
                            _user.registerUser(usuario, DateTime.Now, true);

                            do{
                                Console.WriteLine("Escolha uma das opções abaixo:");
                                Console.WriteLine("1 - Encerrar Sistema");
                                Console.WriteLine("0 - Deslogar");

                                opcoesdelogin = Console.ReadLine();
                                switch (opcoesdelogin)
                                {   
                                    case "1":
                                        encerrar = true;
                                        break;
                                    default:
                                        break;
                                }

                            }while(opcoesdelogin != "0" && encerrar == false);
                            _user.registerUser(usuario, DateTime.Now, false);
                        }
                        break;
                    case "2":
                        Console.WriteLine("Qual o email:");
                        email = Console.ReadLine();
                        Console.WriteLine("Digite sua senha:");
                        senha = Console.ReadLine();
                        Console.WriteLine("Digite sua nome:");
                        nome = Console.ReadLine();
                        User novoUsuario = new(){
                            Email = email,
                            Senha = senha,
                            Nome = nome
                        };
                        _user.Create(novoUsuario);
                        break;

                    default:
                        break;
                }

            } while (option != "0" && encerrar == false);
        }
    }
}