﻿//using Aplicacao.Core.Dominio;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Mail;

//namespace UI.Web.Helpers
//{
//    public class Destinatario
//    {
//        public string Nome { get; set; }
//        public string Email { get; set; }
//    }

//    public class Email
//    {

//        string smtp;
//        int smtp_porta;
//        string email;
//        string senha;
//        string nome;
//        public List<Destinatario> destinatarios = new List<Destinatario>();

//        public void AddDestinatario(string nome, string email)
//        {
//            var Destinatario = new Destinatario();
//            Destinatario.Nome = nome;
//            Destinatario.Email = email;
//            destinatarios.Add(Destinatario);
//        }

//        public Email()
//        {
//            smtp = "smtp.idsites.com.br";
//            smtp_porta = 587;
//            email = "noreply@idsites.com.br";
//            senha = "nor@2016";
//            nome = "nor@2016";
//        }

//        public void SaibaMaisProduto(Contato contato)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);



//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "Seminário Empreendedorismo Magistral");
//            msg.From = new MailAddress(email, "Seminário Empreendedorismo Magistral");
//            msg.To.Add(new MailAddress("web2@consulfarma.com", "Saiba mais - Farmacia Orvalho"));

//            msg.Subject = "Saiba Mais sobre nossos produtos";
//            msg.Body = "<div style=\"line-height: 1px; font-style: Verdana\">";
//            msg.Body += "<p>Olá, o usuário <strong>" + contato.Nome + "</strong> gostaria de saber mais sobre: <p><strong>" + contato.Assunto + "</strong></p></p>";
//            msg.Body += "<p>Dados:</p>";
//            msg.Body += "<p>Nome: <strong>" + contato.Nome + "</strong></p>";
//            msg.Body += "<p>E-Mail: <strong>" + contato.Email + "</strong></p>";
//            msg.Body += "<p>Telefone: <strong>" + contato.Telefone + "</strong></p>";
//            msg.Body += "<p>Celular: <strong>" + contato.Celular + "</strong></p>";
//            msg.Body += "<p>Assunto: <strong>" + contato.Assunto + "</strong></p>";
//            msg.Body += "<br/>";
//            msg.Body += "<p>Mensagem:</p>";
//            msg.Body += "<br/>";
//            msg.Body += contato.Mensagem;
//            msg.Body += "<div>";
//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);

//        }

//        public void Contato(Contato contato)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.Port = 587;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);

//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "Site Finnofarma");
//            msg.From = new MailAddress(email, "Site Finnofarma");
//            msg.To.Add(new MailAddress("web2@consulfarma.com", "Finnofarma"));


//            msg.Subject = "Contato de " + contato.Nome;
//            msg.Body = "<div style=\"line-height: 1px; font-style: Verdana\">";
//            msg.Body += "<p>Olá, o usuário <strong>" + contato.Nome + "</strong> entrou em contato <p><strong>";
//            msg.Body += "<p>Dados:</p>";
//            msg.Body += "<p>Nome: <strong>" + contato.Nome + "</strong></p>";
//            msg.Body += "<p>Email: <strong>" + contato.Email + "</strong></p>";
//            msg.Body += "<p>Telefone: <strong>" + contato.Telefone + "</strong></p>";
//            msg.Body += "<p>Celular: <strong>" + contato.Celular + "</strong></p>";
//            msg.Body += "<p>Setor: <strong>" + contato.Setor + "</strong></p>";
//            msg.Body += "<br/>";
//            msg.Body += "<p>Mensagem:</p>";
//            msg.Body += "<br/>";
//            msg.Body += contato.Mensagem;
//            msg.Body += "<div>";


//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);

//        }

//        public void Orcamento(Orcamento orcamento)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.Port = 587;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);

//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "FarmaciaOrvalho");
//            msg.From = new MailAddress(email, "FarmaciaOrvalho");
//            msg.To.Add(new MailAddress("web2@consulfarma.com", "FarmaciaOrvalho"));


//            msg.Subject = "Pedido de orcamento por - " + orcamento.Nome;
//            msg.Body = "<div style=\"line-height: 1px; font-style: Verdana\">";
//            msg.Body += "<p>Olá, o usuário <strong>" + orcamento.Nome + "</strong> entrou em contato.";
//            msg.Body += "<p>Dados:</p>";
//            msg.Body += "<p>Nome: <strong>" + orcamento.Nome + "</strong></p>";
//            msg.Body += "<p>Email: <strong>" + orcamento.Email + "</strong></p>";
//            msg.Body += "<p>Telefone: <strong>" + orcamento.Telefone + "</strong></p>";
//            msg.Body += "<p>Celular: <strong>" + orcamento.Celular + "</strong></p>";
//            msg.Body += "<br/>";

//            if (orcamento.Delivery)
//            {
//                msg.Body += "<p>Informações para entrega do orçamento:</p>";
//                msg.Body += "<br/>";

//                msg.Body += "<p>Cep: <strong>" + orcamento.Cep + "</strong></p>";
//                msg.Body += "<p>Endereço: <strong>" + orcamento.Endereco + "</strong></p>";
//                msg.Body += "<p>Numero: <strong>" + orcamento.Numero + "</strong></p>";
//                msg.Body += "<p>Complemento: <strong>" + orcamento.Complemento + "</strong></p>";
//                msg.Body += "<p>Bairro: <strong>" + orcamento.Bairro + "</strong></p>";
//                msg.Body += "<p>Cidade: <strong>" + orcamento.Cidade + "</strong></p>";
//                msg.Body += "<p>UF: <strong>" + orcamento.Uf + "</strong></p>";
//            }
//            else
//            {
//                msg.Body += "<p>Não foi solicitado entrega para este orçamento</p>";
//            }
//            msg.Body += "<p>Receita: <a href=\"http://localhost:8511/Arquivos/Receita/" + orcamento.Receita + "\">Clique aqui para abrir a receita</a></p>";
//            msg.Body += "<p>Mensagem: <strong>" + orcamento.Mensagem + "</strong></p>";
//            msg.Body += "<div>";

//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);

//        }

//        public void AlertaNovoAssinante(News news)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.Port = smtp_porta;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);

//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "FarmaciaOrvalho");
//            msg.From = new MailAddress(email, "FarmaciaOrvalho");
//            msg.To.Add(new MailAddress("web2@consulfarma.com", "FarmaciaOrvalho"));

//            msg.Subject = "Cadastro Newsletter - " + news.Nome;
//            msg.Body = "<div style=\"line-height: 1px; font-style: Verdana\">";
//            msg.Body += "<p>Olá, o usuário <strong>" + news.Nome + "</strong> está se cadastrando para receber novidades.</p>";
//            msg.Body += "<div>";

//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);
//        }

//        public void OrcamentoOnline(Contato contato)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.Port = 587;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);

//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "Site Rosa Manipulação");
//            msg.From = new MailAddress(email, "Site Rosa Manipulação");
//            msg.To.Add(new MailAddress("vinicius431@gmail.com", "Site Rosa Manipulação"));

//            msg.Subject = "Orçamento Online " + contato.Assunto;
//            msg.Body = template("Orçamento Online", "Nos envie sua receita e embre iremos retornar o orçamento", contato.Nome, contato.Email, contato.Telefone, contato.Cidade, contato.Estado, contato.Anexo, contato.Mensagem);


//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);
//        }

//        public string template(string titulo, string subtitulo, string nome, string email, string telefone, string cidade, string estado, string anexo, string mensagem)
//        {
//            // BuildMyString.com generated code. Please enjoy your string responsibly.

//            string sb = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">" +
//            "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
//            "<head>" +
//            "	<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />" +
//            "	<meta name=\"viewport\" content=\"width=device-width\"/>" +
//            "	<style>" +
//            "/**********************************************" +
//            "* Ink v1.0.5 - Copyright 2013 ZURB Inc        *" +
//            "**********************************************/" +
//            "/* Client-specific Styles & Reset */" +
//            "#outlook a {" +
//            "	padding:0;" +
//            "}" +
//            "body{" +
//            "	width:100% !important;" +
//            "	min-width: 100%;" +
//            "	-webkit-text-size-adjust:100%;" +
//            "	-ms-text-size-adjust:100%;" +
//            "	margin:0;" +
//            "	padding:0;" +
//            "}" +
//            ".ExternalClass {" +
//            "	width:100%;" +
//            "}" +
//            ".ExternalClass," +
//            ".ExternalClass p," +
//            ".ExternalClass span," +
//            ".ExternalClass font," +
//            ".ExternalClass td," +
//            ".ExternalClass div {" +
//            "	line-height: 100%;" +
//            "}" +
//            "#backgroundTable {" +
//            "	margin:0;" +
//            "	padding:0;" +
//            "	width:100% !important;" +
//            "	line-height: 100% !important;" +
//            "}" +
//            "img {" +
//            "	outline:none;" +
//            "	text-decoration:none;" +
//            "	-ms-interpolation-mode: bicubic;" +
//            "	width: auto;" +
//            "	max-width: 100%;" +
//            "	float: left;" +
//            "	clear: both;" +
//            "	display: block;" +
//            "}" +
//            "center {" +
//            "	width: 100%;" +
//            "	min-width: 580px;" +
//            "}" +
//            "a img {" +
//            "	border: none;" +
//            "}" +
//            "p {" +
//            "	margin: 0 0 0 10px;" +
//            "}" +
//            "table {" +
//            "	border-spacing: 0;" +
//            "	border-collapse: collapse;" +
//            "}" +
//            "td {" +
//            "	word-break: break-word;" +
//            "	-webkit-hyphens: auto;" +
//            "	-moz-hyphens: auto;" +
//            "	hyphens: auto;" +
//            "	border-collapse: collapse !important;" +
//            "}" +
//            "table, tr, td {" +
//            "	padding: 0;" +
//            "	vertical-align: top;" +
//            "	text-align: left;" +
//            "}" +
//            "hr {" +
//            "	color: #d9d9d9;" +
//            "	background-color: #d9d9d9;" +
//            "	height: 1px;" +
//            "	border: none;" +
//            "}" +
//            "/* Responsive Grid */" +
//            "table.body {" +
//            "	height: 100%;" +
//            "	width: 100%;" +
//            "}" +
//            "table.container {" +
//            "	width: 580px;" +
//            "	margin: 0 auto;" +
//            "	text-align: inherit;" +
//            "}" +
//            "table.row {" +
//            "	padding: 0px;" +
//            "	width: 100%;" +
//            "	position: relative;" +
//            "}" +
//            "table.container table.row {" +
//            "	display: block;" +
//            "}" +
//            "td.wrapper {" +
//            "	padding: 10px 20px 0px 0px;" +
//            "	position: relative;" +
//            "}" +
//            "table.columns," +
//            "table.column {" +
//            "	margin: 0 auto;" +
//            "}" +
//            "table.columns td," +
//            "table.column td {" +
//            "	padding: 0px 0px 10px;" +
//            "}" +
//            "table.columns td.sub-columns," +
//            "table.column td.sub-columns," +
//            "table.columns td.sub-column," +
//            "table.column td.sub-column {" +
//            "	padding-right: 10px;" +
//            "}" +
//            "td.sub-column, td.sub-columns {" +
//            "	min-width: 0px;" +
//            "}" +
//            "table.row td.last," +
//            "table.container td.last {" +
//            "	padding-right: 0px;" +
//            "}" +
//            "table.one { width: 30px; }" +
//            "table.two { width: 80px; }" +
//            "table.three { width: 130px; }" +
//            "table.four { width: 180px; }" +
//            "table.five { width: 230px; }" +
//            "table.six { width: 280px; }" +
//            "table.seven { width: 330px; }" +
//            "table.eight { width: 380px; }" +
//            "table.nine { width: 430px; }" +
//            "table.ten { width: 480px; }" +
//            "table.eleven { width: 530px; }" +
//            "table.twelve { width: 580px; }" +
//            "table.one center { min-width: 30px; }" +
//            "table.two center { min-width: 80px; }" +
//            "table.three center { min-width: 130px; }" +
//            "table.four center { min-width: 180px; }" +
//            "table.five center { min-width: 230px; }" +
//            "table.six center { min-width: 280px; }" +
//            "table.seven center { min-width: 330px; }" +
//            "table.eight center { min-width: 380px; }" +
//            "table.nine center { min-width: 430px; }" +
//            "table.ten center { min-width: 480px; }" +
//            "table.eleven center { min-width: 530px; }" +
//            "table.twelve center { min-width: 580px; }" +
//            "table.one .panel center { min-width: 10px; }" +
//            "table.two .panel center { min-width: 60px; }" +
//            "table.three .panel center { min-width: 110px; }" +
//            "table.four .panel center { min-width: 160px; }" +
//            "table.five .panel center { min-width: 210px; }" +
//            "table.six .panel center { min-width: 260px; }" +
//            "table.seven .panel center { min-width: 310px; }" +
//            "table.eight .panel center { min-width: 360px; }" +
//            "table.nine .panel center { min-width: 410px; }" +
//            "table.ten .panel center { min-width: 460px; }" +
//            "table.eleven .panel center { min-width: 510px; }" +
//            "table.twelve .panel center { min-width: 560px; }" +
//            ".body .columns td.one," +
//            ".body .column td.one { width: 8.333333%; }" +
//            ".body .columns td.two," +
//            ".body .column td.two { width: 16.666666%; }" +
//            ".body .columns td.three," +
//            ".body .column td.three { width: 25%; }" +
//            ".body .columns td.four," +
//            ".body .column td.four { width: 33.333333%; }" +
//            ".body .columns td.five," +
//            ".body .column td.five { width: 41.666666%; }" +
//            ".body .columns td.six," +
//            ".body .column td.six { width: 50%; }" +
//            ".body .columns td.seven," +
//            ".body .column td.seven { width: 58.333333%; }" +
//            ".body .columns td.eight," +
//            ".body .column td.eight { width: 66.666666%; }" +
//            ".body .columns td.nine," +
//            ".body .column td.nine { width: 75%; }" +
//            ".body .columns td.ten," +
//            ".body .column td.ten { width: 83.333333%; }" +
//            ".body .columns td.eleven," +
//            ".body .column td.eleven { width: 91.666666%; }" +
//            ".body .columns td.twelve," +
//            ".body .column td.twelve { width: 100%; }" +
//            "td.offset-by-one { padding-left: 50px; }" +
//            "td.offset-by-two { padding-left: 100px; }" +
//            "td.offset-by-three { padding-left: 150px; }" +
//            "td.offset-by-four { padding-left: 200px; }" +
//            "td.offset-by-five { padding-left: 250px; }" +
//            "td.offset-by-six { padding-left: 300px; }" +
//            "td.offset-by-seven { padding-left: 350px; }" +
//            "td.offset-by-eight { padding-left: 400px; }" +
//            "td.offset-by-nine { padding-left: 450px; }" +
//            "td.offset-by-ten { padding-left: 500px; }" +
//            "td.offset-by-eleven { padding-left: 550px; }" +
//            "td.expander {" +
//            "	visibility: hidden;" +
//            "	width: 0px;" +
//            "	padding: 0 !important;" +
//            "}" +
//            "table.columns .text-pad," +
//            "table.column .text-pad {" +
//            "	padding-left: 10px;" +
//            "	padding-right: 10px;" +
//            "}" +
//            "table.columns .left-text-pad," +
//            "table.columns .text-pad-left," +
//            "table.column .left-text-pad," +
//            "table.column .text-pad-left {" +
//            "	padding-left: 10px;" +
//            "}" +
//            "table.columns .right-text-pad," +
//            "table.columns .text-pad-right," +
//            "table.column .right-text-pad," +
//            "table.column .text-pad-right {" +
//            "	padding-right: 10px;" +
//            "}" +
//            "/* Block Grid */" +
//            ".block-grid {" +
//            "	width: 100%;" +
//            "	max-width: 580px;" +
//            "}" +
//            ".block-grid td {" +
//            "	display: inline-block;" +
//            "	padding:10px;" +
//            "}" +
//            ".two-up td {" +
//            "	width:270px;" +
//            "}" +
//            ".three-up td {" +
//            "	width:173px;" +
//            "}" +
//            ".four-up td {" +
//            "	width:125px;" +
//            "}" +
//            ".five-up td {" +
//            "	width:96px;" +
//            "}" +
//            ".six-up td {" +
//            "	width:76px;" +
//            "}" +
//            ".seven-up td {" +
//            "	width:62px;" +
//            "}" +
//            ".eight-up td {" +
//            "	width:52px;" +
//            "}" +
//            "/* Alignment & Visibility Classes */" +
//            "table.center, td.center {" +
//            "	text-align: center;" +
//            "}" +
//            "h1.center," +
//            "h2.center," +
//            "h3.center," +
//            "h4.center," +
//            "h5.center," +
//            "h6.center {" +
//            "	text-align: center;" +
//            "}" +
//            "span.center {" +
//            "	display: block;" +
//            "	width: 100%;" +
//            "	text-align: center;" +
//            "}" +
//            "img.center {" +
//            "	margin: 0 auto;" +
//            "	float: none;" +
//            "}" +
//            ".show-for-small," +
//            ".hide-for-desktop {" +
//            "	display: none;" +
//            "}" +
//            "/* Typography */" +
//            "body, table.body, h1, h2, h3, h4, h5, h6, p, td {" +
//            "	color: #606060;" +
//            "	font-family: \"Helvetica\", \"Arial\", sans-serif;" +
//            "	font-weight: normal;" +
//            "	padding:0;" +
//            "	margin: 0;" +
//            "	text-align: left;" +
//            "	line-height: 1.4;" +
//            "}" +
//            "h1, h2, h3, h4, h5, h6 {" +
//            "	color: #525252;" +
//            "	word-break: normal;" +
//            "	margin-top: 10px;" +
//            "	margin-bottom: 10px;" +
//            "}" +
//            "h1 {font-size: 40px;}" +
//            "h2 {font-size: 36px;}" +
//            "h3 {font-size: 32px;}" +
//            "h4 {font-size: 28px;}" +
//            "h5 {font-size: 24px;}" +
//            "h6 {font-size: 20px;}" +
//            "body, table.body, p, td {font-size: 16px;line-height:24px;}" +
//            "p.lead, p.lede, p.leed {" +
//            "	font-size: 18px;" +
//            "	line-height: 24px;" +
//            "}" +
//            "p.small {" +
//            "	font-size: 14px;" +
//            "	line-height: 20px;" +
//            "	padding: 0 10px !important;" +
//            "	color: #999;" +
//            "}" +
//            "p {" +
//            "	margin-bottom: 10px;" +
//            "}" +
//            "small {" +
//            "	font-size: 10px;" +
//            "}" +
//            "a {" +
//            "	color: #2ba6cb;" +
//            "	text-decoration: none;" +
//            "}" +
//            "a:hover {" +
//            "	color: #2795b6 !important;" +
//            "}" +
//            "a:active {" +
//            "	color: #2795b6 !important;" +
//            "}" +
//            "a:visited {" +
//            "	color: #2ba6cb !important;" +
//            "}" +
//            "h1 a," +
//            "h2 a," +
//            "h3 a," +
//            "h4 a," +
//            "h5 a," +
//            "h6 a {" +
//            "	color: #2ba6cb;" +
//            "}" +
//            "h1 a:active," +
//            "h2 a:active," +
//            "h3 a:active," +
//            "h4 a:active," +
//            "h5 a:active," +
//            "h6 a:active {" +
//            "	color: #2ba6cb !important;" +
//            "}" +
//            "h1 a:visited," +
//            "h2 a:visited," +
//            "h3 a:visited," +
//            "h4 a:visited," +
//            "h5 a:visited," +
//            "h6 a:visited {" +
//            "	color: #2ba6cb !important;" +
//            "}" +
//            "/* Panels */" +
//            ".panel {" +
//            "	background: #ffffff;" +
//            "	border: 1px solid #d9d9d9;" +
//            "	padding: 10px !important;" +
//            "}" +
//            ".sub-grid table {" +
//            "	width: 100%;" +
//            "}" +
//            ".sub-grid td.sub-columns {" +
//            "	padding-bottom: 0;" +
//            "}" +
//            "/* Buttons */" +
//            "table.button," +
//            "table.tiny-button," +
//            "table.small-button," +
//            "table.medium-button," +
//            "table.large-button {" +
//            "	width: 100%;" +
//            "	overflow: hidden;" +
//            "}" +
//            "table.button td," +
//            "table.tiny-button td," +
//            "table.small-button td," +
//            "table.medium-button td," +
//            "table.large-button td {" +
//            "	display: block;" +
//            "	width: auto !important;" +
//            "	text-align: center;" +
//            "	background: #2ba6cb;" +
//            "	border: 1px solid #2284a1;" +
//            "	color: #ffffff;" +
//            "	padding: 8px 0;" +
//            "}" +
//            "table.tiny-button td {" +
//            "	padding: 5px 0 4px;" +
//            "}" +
//            "table.small-button td {" +
//            "	padding: 8px 0 7px;" +
//            "}" +
//            "table.medium-button td {" +
//            "	padding: 12px 0 10px;" +
//            "}" +
//            "table.large-button td {" +
//            "	padding: 21px 0 18px;" +
//            "}" +
//            "table.button td a," +
//            "table.tiny-button td a," +
//            "table.small-button td a," +
//            "table.medium-button td a," +
//            "table.large-button td a {" +
//            "	font-weight: bold;" +
//            "	text-decoration: none;" +
//            "	font-family: Helvetica, Arial, sans-serif;" +
//            "	color: #ffffff;" +
//            "	font-size: 16px;" +
//            "}" +
//            "table.tiny-button td a {" +
//            "	font-size: 12px;" +
//            "	font-weight: normal;" +
//            "}" +
//            "table.small-button td a {" +
//            "	font-size: 16px;" +
//            "}" +
//            "table.medium-button td a {" +
//            "	font-size: 20px;" +
//            "}" +
//            "table.large-button td a {" +
//            "	font-size: 24px;" +
//            "}" +
//            "table.button:hover td," +
//            "table.button:visited td," +
//            "table.button:active td {" +
//            "	background: #2795b6 !important;" +
//            "}" +
//            "table.button:hover td a," +
//            "table.button:visited td a," +
//            "table.button:active td a {" +
//            "	color: #fff !important;" +
//            "}" +
//            "table.button:hover td," +
//            "table.tiny-button:hover td," +
//            "table.small-button:hover td," +
//            "table.medium-button:hover td," +
//            "table.large-button:hover td {" +
//            "	background: #2795b6 !important;" +
//            "}" +
//            "table.button:hover td a," +
//            "table.button:active td a," +
//            "table.button td a:visited," +
//            "table.tiny-button:hover td a," +
//            "table.tiny-button:active td a," +
//            "table.tiny-button td a:visited," +
//            "table.small-button:hover td a," +
//            "table.small-button:active td a," +
//            "table.small-button td a:visited," +
//            "table.medium-button:hover td a," +
//            "table.medium-button:active td a," +
//            "table.medium-button td a:visited," +
//            "table.large-button:hover td a," +
//            "table.large-button:active td a," +
//            "table.large-button td a:visited {" +
//            "	color: #ffffff !important;" +
//            "}" +
//            "table.secondary td {" +
//            "	background: #e9e9e9;" +
//            "	border-color: #d0d0d0;" +
//            "	color: #555;" +
//            "}" +
//            "table.secondary td a {" +
//            "	color: #555;" +
//            "}" +
//            "table.secondary:hover td {" +
//            "	background: #d0d0d0 !important;" +
//            "	color: #555;" +
//            "}" +
//            "table.secondary:hover td a," +
//            "table.secondary td a:visited," +
//            "table.secondary:active td a {" +
//            "	color: #555 !important;" +
//            "}" +
//            "table.success td {" +
//            "	background: #5da423;" +
//            "	border-color: #457a1a;" +
//            "}" +
//            "table.success:hover td {" +
//            "	background: #457a1a !important;" +
//            "}" +
//            "table.alert td {" +
//            "	background: #c60f13;" +
//            "	border-color: #970b0e;" +
//            "}" +
//            "table.alert:hover td {" +
//            "	background: #970b0e !important;" +
//            "}" +
//            "table.radius td {" +
//            "	-webkit-border-radius: 3px;" +
//            "	-moz-border-radius: 3px;" +
//            "	border-radius: 3px;" +
//            "}" +
//            "table.round td {" +
//            "	-webkit-border-radius: 500px;" +
//            "	-moz-border-radius: 500px;" +
//            "	border-radius: 500px;" +
//            "}" +
//            "/* Outlook First */" +
//            "body.outlook p {" +
//            "	display: inline !important;" +
//            "}" +
//            "/*  Media Queries */" +
//            "@media only screen and (max-width: 600px) {" +
//            "	table[class=\"body\"] img {" +
//            "		width: auto !important;" +
//            "		height: auto !important;" +
//            "	}" +
//            "	table[class=\"body\"] center {" +
//            "		min-width: 0 !important;" +
//            "	}" +
//            "	table[class=\"body\"] .container {" +
//            "		width: 95% !important;" +
//            "	}" +
//            "	table[class=\"body\"] .row {" +
//            "		width: 100% !important;" +
//            "		display: block !important;" +
//            "	}" +
//            "	table[class=\"body\"] .wrapper {" +
//            "		display: block !important;" +
//            "		padding-right: 0 !important;" +
//            "	}" +
//            "	table[class=\"body\"] .columns," +
//            "	table[class=\"body\"] .column {" +
//            "		table-layout: fixed !important;" +
//            "		float: none !important;" +
//            "		width: 100% !important;" +
//            "		padding-right: 0px !important;" +
//            "		padding-left: 0px !important;" +
//            "		display: block !important;" +
//            "	}" +
//            "	table[class=\"body\"] .wrapper.first .columns," +
//            "	table[class=\"body\"] .wrapper.first .column {" +
//            "		display: table !important;" +
//            "	}" +
//            "	table[class=\"body\"] table.columns td," +
//            "	table[class=\"body\"] table.column td {" +
//            "		width: 100% !important;" +
//            "	}" +
//            "	table[class=\"body\"] .columns td.one," +
//            "	table[class=\"body\"] .column td.one { width: 8.333333% !important; }" +
//            "	table[class=\"body\"] .columns td.two," +
//            "	table[class=\"body\"] .column td.two { width: 16.666666% !important; }" +
//            "	table[class=\"body\"] .columns td.three," +
//            "	table[class=\"body\"] .column td.three { width: 25% !important; }" +
//            "	table[class=\"body\"] .columns td.four," +
//            "	table[class=\"body\"] .column td.four { width: 33.333333% !important; }" +
//            "	table[class=\"body\"] .columns td.five," +
//            "	table[class=\"body\"] .column td.five { width: 41.666666% !important; }" +
//            "	table[class=\"body\"] .columns td.six," +
//            "	table[class=\"body\"] .column td.six { width: 50% !important; }" +
//            "	table[class=\"body\"] .columns td.seven," +
//            "	table[class=\"body\"] .column td.seven { width: 58.333333% !important; }" +
//            "	table[class=\"body\"] .columns td.eight," +
//            "	table[class=\"body\"] .column td.eight { width: 66.666666% !important; }" +
//            "	table[class=\"body\"] .columns td.nine," +
//            "	table[class=\"body\"] .column td.nine { width: 75% !important; }" +
//            "	table[class=\"body\"] .columns td.ten," +
//            "	table[class=\"body\"] .column td.ten { width: 83.333333% !important; }" +
//            "	table[class=\"body\"] .columns td.eleven," +
//            "	table[class=\"body\"] .column td.eleven { width: 91.666666% !important; }" +
//            "	table[class=\"body\"] .columns td.twelve," +
//            "	table[class=\"body\"] .column td.twelve { width: 100% !important; }" +
//            "	table[class=\"body\"] td.offset-by-one," +
//            "	table[class=\"body\"] td.offset-by-two," +
//            "	table[class=\"body\"] td.offset-by-three," +
//            "	table[class=\"body\"] td.offset-by-four," +
//            "	table[class=\"body\"] td.offset-by-five," +
//            "	table[class=\"body\"] td.offset-by-six," +
//            "	table[class=\"body\"] td.offset-by-seven," +
//            "	table[class=\"body\"] td.offset-by-eight," +
//            "	table[class=\"body\"] td.offset-by-nine," +
//            "	table[class=\"body\"] td.offset-by-ten," +
//            "	table[class=\"body\"] td.offset-by-eleven {" +
//            "		padding-left: 0 !important;" +
//            "	}" +
//            "	table[class=\"body\"] table.columns td.expander {" +
//            "		width: 1px !important;" +
//            "	}" +
//            "	table[class=\"body\"] .right-text-pad," +
//            "	table[class=\"body\"] .text-pad-right {" +
//            "		padding-left: 10px !important;" +
//            "	}" +
//            "	table[class=\"body\"] .left-text-pad," +
//            "	table[class=\"body\"] .text-pad-left {" +
//            "		padding-right: 10px !important;" +
//            "	}" +
//            "	table[class=\"body\"] .hide-for-small," +
//            "	table[class=\"body\"] .show-for-desktop {" +
//            "		display: none !important;" +
//            "	}" +
//            "	table[class=\"body\"] .show-for-small," +
//            "	table[class=\"body\"] .hide-for-desktop {" +
//            "		display: inherit !important;" +
//            "	}" +
//            "}" +
//            "	</style>" +
//            "	<style>" +
//            "		table.facebook td {" +
//            "			background: #3b5998;" +
//            "			border-color: #2d4473;" +
//            "		}" +
//            "		table.facebook:hover td {" +
//            "			background: #2d4473 !important;" +
//            "		}" +
//            "		table.twitter td {" +
//            "			background: #00acee;" +
//            "			border-color: #0087bb;" +
//            "		}" +
//            "		table.twitter:hover td {" +
//            "			background: #0087bb !important;" +
//            "		}" +
//            "		table.google-plus td {" +
//            "			background-color: #DB4A39;" +
//            "			border-color: #CC0000;" +
//            "		}" +
//            "		table.google-plus:hover td {" +
//            "			background: #CC0000 !important;" +
//            "		}" +
//            "		.template-label {" +
//            "			color: #ffffff;" +
//            "			font-weight: bold;" +
//            "			font-size: 11px;" +
//            "		}" +
//            "		.callout .wrapper {" +
//            "			padding-bottom: 20px;" +
//            "		}" +
//            "		.callout .panel {" +
//            "			background: #ECF8FF;" +
//            "			border-color: #b9e5ff;" +
//            "		}" +
//            "		.header {" +
//            "			background: #232831;" +
//            "		}" +
//            "		.footer .wrapper {" +
//            "			background: #ebebeb;" +
//            "		}" +
//            "		.footer h5 {" +
//            "			padding-bottom: 10px;" +
//            "		}" +
//            "		table.columns .text-pad {" +
//            "			padding-left: 10px;" +
//            "			padding-right: 10px;" +
//            "		}" +
//            "		table.columns .left-text-pad {" +
//            "			padding-left: 10px;" +
//            "		}" +
//            "		table.columns .right-text-pad {" +
//            "			padding-right: 10px;" +
//            "		}" +
//            "		@media only screen and (max-width: 600px) {" +
//            "			table[class=\"body\"] .right-text-pad {" +
//            "				padding-left: 10px !important;" +
//            "			}" +
//            "			table[class=\"body\"] .left-text-pad {" +
//            "				padding-right: 10px !important;" +
//            "			}" +
//            "		}" +
//            "		.white-link{" +
//            "			color: #ffffff;" +
//            "		}" +
//            "		/***" +
//            "		Social Icons" +
//            "		***/" +
//            "		.social-icons {" +
//            "			float: right;" +
//            "		}" +
//            "		.social-icons td {" +
//            "			padding: 0 3px !important;" +
//            "			width: auto !important;" +
//            "		}" +
//            "		.social-icons td img{" +
//            "			width: 24px;" +
//            "			height: 24px;" +
//            "		}" +
//            "		.social-icons td:last-child {" +
//            "			padding-right: 0 !important;" +
//            "		}" +
//            "		.social-icons td img {" +
//            "			max-width: none !important;" +
//            "		}" +
//            "		table[class=\"body\"] table.columns .social-icons td {" +
//            "			width: auto !important;" +
//            "		}" +
//            "		.contact p{" +
//            "			text-align: right !important;" +
//            "			font-size: 11px !important;" +
//            "			line-height: 18px !important;" +
//            "			color: #bfbfbf;" +
//            "		}" +
//            "		.contact .padding{" +
//            "			padding-top: 40px;" +
//            "		}" +
//            "		.contact-links{" +
//            "			padding-top: 50px;" +
//            "		}" +
//            "		.contact-links td{" +
//            "			padding: 5px 0 10px 0 !important;" +
//            "		}" +
//            "		.contact-links a{" +
//            "			color: #525252;" +
//            "			font-weight: bold;" +
//            "			line-height: 125%;" +
//            "			margin-left: 10px;" +
//            "		}" +
//            "	</style>" +
//            "</head>" +
//            "<body>" +
//            "	<table class=\"body\">" +
//            "		<tr>" +
//            "			<td class=\"center\" align=\"center\" valign=\"top\">" +
//            "				<center>" +
//            "					<table class=\"row header\">" +
//            "						<tr>" +
//            "							<td class=\"center\" align=\"center\">" +
//            "								<center>" +
//            "									<table class=\"container\">" +
//            "										<tr>" +
//            "											<td class=\"wrapper last\">" +
//            "												<table class=\"twelve columns\">" +
//            "													<tr>" +
//            "														<td style>" +
//            "															" +
//            "														</td>" +
//            "														<td class=\"six sub-columns last\" style=\"text-align:right; vertical-align:middle;\">" +
//            "                                                            <a href=\"#\"><img src=\"" + "http://localhost:8511/" + "img/rosa-farmacia.png\" alt=\"\" style=\"max-width: 300px; margin: 0 auto; text-align: center\" /></a>    " +
//            "														</td>" +
//            "														<td class=\"expander\"></td>" +
//            "													</tr>" +
//            "												</table>" +
//            "											</td>" +
//            "										</tr>" +
//            "									</table>" +
//            "								</center>" +
//            "							</td>" +
//            "						</tr>" +
//            "					</table>" +
//            "					<table class=\"container\">" +
//            "						<tr>" +
//            "							<td>" +
//            "								<table class=\"row\">" +
//            "									<tr>" +
//            "										<td class=\"wrapper last\">" +
//            "											<table class=\"twelve columns\">" +
//            "												<tr>" +
//            "													<td>" +
//            "														<h1>VAR_TITULO</h1>" +
//            "														<p class=\"lead\">VAR_SUBTITULO Um novo orçamento online foi enviado para a farmácia.</p>" +
//            "													</td>" +
//            "													<td class=\"expander\"></td>" +
//            "												</tr>" +
//            "												<tr>" +
//            "													<td class=\"panel\">" +
//            "														<h6>Nome</h6>" +
//            "                                                        <p>VAR_NOME</p>" +
//            "                                                        <h6>Email</h6>" +
//            "                                                        <p>VAR_EMAIL</p>" +
//            "                                                        <h6>Telefone</h6>" +
//            "                                                        <p>VAR_TELEFONE</p>" +
//            "                                                        <h6>Cidade</h6>" +
//            "                                                        <p>VAR_CIDADE</p>" +
//            "                                                        <h6>Estado</h6>" +
//            "                                                        <p>VAR_ESTADO</p>" +
//            "                                                        <h6>Anexo</h6>" +
//            "                                                        <p><a href=\"VAR_ANEXO\">Clique aqui para baixar</a></p>" +
//            "                                                        <h6>Mensagem</h6>" +
//            "                                                        <p>VAR_MENSAGEM</p>" +
//            "													</td>" +
//            "													<td class=\"expander\"></td>" +
//            "												</tr>" +
//            "											</table>" +
//            "										</td>" +
//            "									</tr>" +
//            "								</table>" +
//            "							<!-- container end below -->" +
//            "							</td>" +
//            "						</tr>" +
//            "					</table>" +
//            "				</center>" +
//            "			</td>" +
//            "		</tr>" +
//            "	</table>" +
//            "</body>" +
//            "</html>";

//            sb = sb.Replace("VAR_TITULO", titulo);
//            sb = sb.Replace("VAR_SUBTITULO", subtitulo);

//            sb = sb.Replace("VAR_NOME", nome);
//            sb = sb.Replace("VAR_EMAIL", email);
//            sb = sb.Replace("VAR_TELEFONE", telefone);
//            sb = sb.Replace("VAR_CIDADE", cidade);
//            sb = sb.Replace("VAR_ESTADO", estado);
//            sb = sb.Replace("VAR_ANEXO", anexo);
//            sb = sb.Replace("VAR_MENSAGEM", mensagem);

//            return sb;


//        }

//        public void Franquia(Franquia Franquia)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.Port = 587;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);

//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "Site Finnofarma");
//            msg.From = new MailAddress(email, "Site Finnofarma");
//            msg.To.Add(new MailAddress("web2@consulfarma.com", "Finnofarma"));

//            msg.Subject = "Solicitação de Franquia";
//            msg.Body = "<div style=\"line-height: 1px; font-style: Verdana\">";
//            msg.Body += "<p>Olá, o usuário <strong>" + Franquia.Nome + "</strong> entrou em contato pela tela: <p><strong>";
//            msg.Body += "<p>Dados:</p>";
//            msg.Body += "<p>Escolha o Estado: <strong>" + Franquia.UfFranquia + "</strong></p>";
//            msg.Body += "<p>Escolha a Cidade: <strong>" + Franquia.CidadeFranquia + "</strong></p>";

//            msg.Body += "<p>Dados Pessoais:</p>";
//            msg.Body += "<p>Nome: <strong>" + Franquia.Nome + "</strong></p>";
//            msg.Body += "<p>Email: <strong>" + Franquia.Email + "</strong></p>";
//            msg.Body += "<p>Data de Nascimento: <strong>" + Franquia.DataNasc + "</strong></p>";
//            msg.Body += "<p>Sexo: <strong>" + Franquia.Sexo + "</strong></p>";
//            msg.Body += "<p>Identidade: <strong>" + Franquia.Identidade + "</strong></p>";
//            msg.Body += "<p>CPF: <strong>" + Franquia.Cpf + "</strong></p>";
//            msg.Body += "<p>Estado Civil: <strong>" + Franquia.EstadoCivil + "</strong></p>";
//            msg.Body += "<p>Endereço: <strong>" + Franquia.Endereco + "</strong></p>";
//            msg.Body += "<p>Numero: <strong>" + Franquia.Numero + "</strong></p>";
//            msg.Body += "<p>Complemento: <strong>" + Franquia.Complemento + "</strong></p>";
//            msg.Body += "<p>Bairro: <strong>" + Franquia.Bairro + "</strong></p>";
//            msg.Body += "<p>Estado: <strong>" + Franquia.UfPessoa + "</strong></p>";
//            msg.Body += "<p>Cidade: <strong>" + Franquia.CidadePessoa + "</strong></p>";
//            msg.Body += "<p>Cep: <strong>" + Franquia.CepPessoa + "</strong></p>";
//            msg.Body += "<p>Telefone: <strong>" + Franquia.TelPessoa + "</strong></p>";
//            msg.Body += "<p>Celular: <strong>" + Franquia.CelularPessoa + "</strong></p>";

//            msg.Body += "<p>Dados Profissionais:</p>";
//            msg.Body += "<p>Capital próprio para investimento (sem considerar financiamentos):<strong>" + Franquia.CapitalInvestimento + "</strong></p>";
//            msg.Body += "<p>Utilizará financiamento bancário?: <strong>" + Franquia.FinanciamentoBancario + "</strong></p>";
//            msg.Body += "<p>Porcentagem: <strong>" + Franquia.Porcentagem + "</strong></p>";
//            msg.Body += "<p>Principais fontes de renda atualmente: <strong>" + Franquia.FonteRenda + "</strong></p>";
//            msg.Body += "<p>Manterá outras atividades além da franquia? Quais? <strong>" + Franquia.OutrasAtividades + "</strong></p>";
//            msg.Body += "<p>Quanto tempo dedicado à operação da franquia? <strong>" + Franquia.TempoDedicado + "</strong></p>";
//            msg.Body += "<p>Possui ponto comercial? Comente. <strong>" + Franquia.PontoComercial + "</strong></p>";
//            msg.Body += "<p>Como surgiu o interesse pela Finnofarma? <strong>" + Franquia.Interesse + "</strong></p>";

//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);

//        }

//        public void ContatoHome(Contato contato)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.Port = smtp_porta;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);

//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "FarmaciaOrvalho");
//            msg.From = new MailAddress(email, "FarmaciaOrvalho");
//            msg.To.Add(new MailAddress("web2@consulfarma.com", "FarmaciaOrvalho"));

//            msg.Subject = "Contato " + contato.Assunto;
//            msg.Body = "<div style=\"line-height: 1px; font-style: Verdana\">";
//            msg.Body += "<p>Olá, o usuário <strong>" + contato.Nome + "</strong> entrou em contato pela tela: <p><strong>" + contato.Assunto + "</strong></p></p>";
//            msg.Body += "<p>Dados:</p>";
//            msg.Body += "<p>Nome: <strong>" + contato.Nome + "</strong></p>";
//            msg.Body += "<p>Telefone: <strong>" + contato.Telefone + "</strong></p>";
//            msg.Body += "<p>Email: <strong>" + contato.Email + "</strong></p>";
//            msg.Body += "<p>Assunto: <strong>" + contato.Assunto + "</strong></p>";
//            msg.Body += "<br/>";
//            msg.Body += "<p>Mensagem:</p>";
//            msg.Body += "<br/>";
//            msg.Body += contato.Mensagem;
//            msg.Body += "<div>";

//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);
//        }

//        public void ContatoSaibaMais(Contato contato)
//        {

//            SmtpClient cliente = new SmtpClient();
//            cliente.Host = smtp;
//            cliente.Port = smtp_porta;
//            cliente.EnableSsl = false;
//            cliente.Credentials = new NetworkCredential(email, senha);

//            MailMessage msg = new MailMessage();
//            msg.Sender = new MailAddress(email, "Site Finnofarma");
//            msg.From = new MailAddress(email, "Site Finnofarma");
//            msg.To.Add(new MailAddress("web2@consulfarma.com", "Finnofarma"));

//            msg.Subject = "Contato " + contato.Assunto;
//            msg.Body = "<div style=\"line-height: 1px; font-style: Verdana\">";
//            msg.Body += "<p>Olá, o usuário <strong>" + contato.Nome + "</strong> entrou em contato pela tela: <p><strong>" + contato.Assunto + "</strong></p></p>";
//            msg.Body += "<p>Dados:</p>";
//            msg.Body += "<p>Nome: <strong>" + contato.Nome + "</strong></p>";
//            msg.Body += "<p>Email: <strong>" + contato.Email + "</strong></p>";
//            msg.Body += "<p>Telefone: <strong>" + contato.Telefone + "</strong></p>";
//            msg.Body += "<p>Telefone: <strong>" + contato.Celular + "</strong></p>";
//            msg.Body += "<p>Assunto: <strong>" + contato.Assunto + "</strong></p>";
//            msg.Body += "<br/>";
//            msg.Body += "<p>Comentário sobre o produto:</p>";
//            msg.Body += "<br/>";
//            msg.Body += contato.Mensagem;
//            msg.Body += "<div>";

//            msg.IsBodyHtml = true;
//            msg.Priority = MailPriority.High;
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            cliente.Send(msg);
//        }
//    }
//}