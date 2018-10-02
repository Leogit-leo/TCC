using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF;
using Aplicacao.Core.RepositorioEF.Contexto;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Web.Attributes;
using UI.Web.Helpers;

namespace UI.Web.Areas.Admin.Controllers
{
    [OnlyAdminAttibute]
    [ValidateInput(false)]
    [CustomAuthorize]
    public class FuncionarioController : Controller
    {
        BancoContexto contexto;

        public FuncionarioController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "Funcionario";
            ViewBag.TitulosForm = "Funcionario";
            ViewBag.tblDinamico = "tblFuncionarios";
            ViewBag.Status = contexto.Status.ToList();
        }

        public ActionResult Index(int? page , string s)
        {
            s = (s != null) ? s.ToLower() : null;

            var Funcionarios = contexto.Funcionario.Where(x => x.Nome.ToLower().Contains(s) || s == null  ).OrderByDescending(x => x.FuncionarioId).ToPagedList(page ?? 1, 10);
            return View(Funcionarios);
        }

        public ActionResult Create()
        {
            ViewBag.Servicos = contexto.Servico.OrderBy(x => x.ServicoId).ToList();
            return View();
        }
         
        [HttpPost]
        public JsonResult Create(Funcionario funcionario , FormCollection collection , HttpPostedFileBase file)
        {
            var Retorno = new RetornoJson();

            if (funcionario.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
            if (funcionario.Endereco == null)
                Retorno.Mensagem += "<span> Digite o Endereço</span>";
            if (funcionario.Telefone == null)
                Retorno.Mensagem += "<span> Digite o Telefone</span>";
            if(funcionario.Salario <=0)
                Retorno.Mensagem += "<span> Digite o Salario</span>";
            if (funcionario.Login == null)
                Retorno.Mensagem += "<span> Digite o Login</span>";
            if (funcionario.Senha == null)
                Retorno.Mensagem += "<span> Digite a Senha</span>";
            if (funcionario.ConfirmarSenha == null)
                Retorno.Mensagem += "<span> Confirmar a Senha</span>";

            if (funcionario.Senha != funcionario.ConfirmarSenha)
                Retorno.Mensagem += "<span> Confirme corretamente sua senha !</span>";
            if (funcionario.Email == null)
                Retorno.Mensagem += "<span> Digite o Email</span>";
            if (funcionario.Tipo == null)
                Retorno.Mensagem += "<span> Informe o Tipo de acesso</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            try
            {
                if(file != null)
                {
                    string nome = "Funcionario-" + NomeArquivo.GerarNomeArquivo(file.FileName);
                    string path = Path.Combine(Server.MapPath(Caminho.Usuario()), Path.GetFileName(nome));
                    file.SaveAs(path);
                    funcionario.Foto = nome;
                }


                var bdFuncionario = new FuncionarioRepositorioEF(contexto);
                bdFuncionario.Adicionar(funcionario);
                bdFuncionario.SalvarTodos();

               //salvando os dados de funcionario e seus servicos que ele realiza na tabela relacional
                
                if(collection["Servico"] != null)
                {
                    var servicos = collection["Servico"].Split(',');

                    var bdRelFuncionarioServico = new RelFuncionarioServicoRepositorioEF(contexto);
                    foreach(var servico in servicos)
                    {
                        bdRelFuncionarioServico.Adicionar(new RelFuncionarioServico()
                        {
                            FuncionarioId = funcionario.FuncionarioId,
                            ServicoId = int.Parse(servico)
                        });
                    }

                    bdRelFuncionarioServico.SalvarTodos();
                }

                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Sucesso = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/Funcionario/Index";

            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Erro no cadastro</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int? id)
        {
            ViewBag.Servicos = contexto.Servico.OrderBy(x => x.ServicoId).ToList();

            ViewBag.Selecionados = contexto.RelFuncionarioServico.Where(x => x.FuncionarioId == id).ToList();

            var Funcionario = contexto.Funcionario.Where(x => x.FuncionarioId == id).FirstOrDefault();
            if (Funcionario == null)
                return RedirectToAction("Index");
            return View(Funcionario);
        }

        [HttpPost]
        public JsonResult Edit(Funcionario funcionario , FormCollection collection , HttpPostedFileBase file , string HiddenSenha , string HiddenConfSenha)
        {
            var Retorno = new RetornoJson();
            if (funcionario.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
            if (funcionario.Endereco == null)
                Retorno.Mensagem += "<span> Digite o Endereço</span>";
            if (funcionario.Telefone == null)
                Retorno.Mensagem += "<span> Digite o Telefone</span>";
            if(funcionario.Salario == 0)
                Retorno.Mensagem += "<span> Digite o Salario</span>";
            if (funcionario.Login == null)
                Retorno.Mensagem += "<span> Digite o Login</span>";
            if (funcionario.Senha != funcionario.ConfirmarSenha)
                Retorno.Mensagem += "<span> As senhas devem ser iguais</span>";
            if (funcionario.Email == null)
                Retorno.Mensagem += "<span> Digite o Email</span>";
            if (funcionario.Tipo == null)
                Retorno.Mensagem += "<span> Informe o Tipo de acesso</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            if (file != null)
            {
                string nome = "Funcionario-" + NomeArquivo.GerarNomeArquivo(file.FileName);
                string path = Path.Combine(Server.MapPath(Caminho.Usuario()), Path.GetFileName(nome));
                file.SaveAs(path);
                funcionario.Foto = nome;
            }
           

            if (funcionario.Senha == null && funcionario.ConfirmarSenha == null)
            {
                funcionario.Senha = HiddenSenha;
                funcionario.ConfirmarSenha = HiddenConfSenha;
            }

            var bdFuncionario = new FuncionarioRepositorioEF(contexto);
            bdFuncionario.Atualizar(funcionario);
            bdFuncionario.SalvarTodos();



            // editar os servicos pertencente ao funcionario da tabela relacional
            
            var bdRelFuncionarioServico = new RelFuncionarioServicoRepositorioEF(contexto);
            var ServicosJaExistentes = contexto.RelFuncionarioServico.Where(x => x.FuncionarioId == funcionario.FuncionarioId).ToList();



                if (collection["Servico"] == null)
            {
                foreach (var servicoExistente in ServicosJaExistentes) {
                    bdRelFuncionarioServico.Excluir(x => x.ServicoId == servicoExistente.ServicoId &&  x.FuncionarioId == funcionario.FuncionarioId);
                    bdRelFuncionarioServico.SalvarTodos();
                }
            }

            if (collection["Servico"] != null)
            {
                var Servicos = collection["Servico"].Split(',');

                foreach (var servico in Servicos)
                {
                    // salvar se os servicos nao existirem no banco de dados
                    var ServicoId = int.Parse(servico);
                    if (!ServicosJaExistentes.Any(x => x.ServicoId == ServicoId))
                    {
                        bdRelFuncionarioServico.Adicionar(new RelFuncionarioServico()
                        {
                            FuncionarioId = funcionario.FuncionarioId,
                            ServicoId = ServicoId
                        });
                    }
                    // excluir se no banco de dados tiver servico que não está na lista

                    foreach (var servicoExistente in ServicosJaExistentes)
                    {
                        if (!Servicos.Any(x => x == servicoExistente.ServicoId.ToString()))
                            bdRelFuncionarioServico.Excluir(x => x.ServicoId == servicoExistente.ServicoId && x.FuncionarioId == funcionario.FuncionarioId);
                    }

                    bdRelFuncionarioServico.SalvarTodos();

                }

            }
            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Funcionario/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

       public ActionResult MudarStatus(int id)
        {
            var funcionario = contexto.Funcionario.FirstOrDefault(x => x.FuncionarioId == id);
            funcionario.StatusId = funcionario.StatusId == 1 ? 2 : 1;
            using(var bdFuncionario = new FuncionarioRepositorioEF(contexto))
            {
                bdFuncionario.Atualizar(funcionario);
                bdFuncionario.SalvarTodos();
            }
            return RedirectToAction("Index");
        }

    }
}