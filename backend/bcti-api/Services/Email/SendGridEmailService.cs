using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using DotNetEnv;

namespace BancoDeConhecimentoInteligenteAPI.Services.Email
{
    public class SendGridEmailService : IEmailService
    {
        private readonly string _apiKey;

        public SendGridEmailService()
        {
            // Carrega o .env (apenas se não estiver em produção com variáveis de ambiente)
            Env.Load(); // lê .env na raiz do projeto

            // Pega a chave diretamente da variável de ambiente
            _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            if (string.IsNullOrEmpty(_apiKey))
                throw new Exception("SENDGRID_API_KEY não foi configurada. Verifique o .env ou variáveis de ambiente.");
        }

        private string GerarTemplateHtml(string nome, string titulo, string subtitulo, string textoBotao, string link)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #031926;
                        color: #fff;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 0 auto;
                        background-color: #fff;
                        color: #000;
                        padding: 20px;
                    }}
                    .header {{
                        text-align: center;
                        padding: 20px 0;
                    }}
                    .header h1 {{
                        font-size: 48px;
                        color: #000;
                        margin: 0;
                    }}
                    .header p {{
                        font-size: 18px;
                        color: #666;
                    }}
                    .content {{
                        text-align: center;
                    }}
                    .content a {{
                        display: inline-block;
                        background-color: #031926;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        font-size: 16px;
                        margin-top: 20px;
                        border-radius: 5px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>CT.IA</h1>
                        <p>B.C.T.I</p>
                    </div>
                    <div class='content'>
                        <strong>Olá {nome}</strong>,<br/><br/>
                        {subtitulo}<br/>
                        <a href='{link}' >{textoBotao}</a>
                    </div>
                </div>
            </body>
            </html>";
        }

        public async Task EnviarConfirmacao(string email, string nome, string linkConfirmacao)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("bcti.no.reply@outlook.com", "CT.IA App");
            var to = new EmailAddress(email, nome);
            var subject = "Confirme seu cadastro na CT.IA";
            var htmlContent = GerarTemplateHtml(nome, "Confirme seu cadastro", "Clique no link abaixo para confirmar seu cadastro:", "Confirmar cadastro", linkConfirmacao);
            var plainTextContent = $"Olá {nome}, clique no link para confirmar seu cadastro: {linkConfirmacao}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Body.ReadAsStringAsync();
                throw new Exception($"Falha ao enviar e-mail: {response.StatusCode} - {body}");
            }
        }

        public async Task EnviarEmailAsync(string email, string nome, string titulo, string subtitulo, string textoBotao, string link)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("bcti.no.reply@outlook.com", "CT.IA App");
            var to = new EmailAddress(email);
            var plainTextContent = "Acesse o link via navegador para continuar.";
            var htmlContent = GerarTemplateHtml(nome, titulo, subtitulo, textoBotao, link);

            var msg = MailHelper.CreateSingleEmail(from, to, titulo, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Body.ReadAsStringAsync();
                throw new Exception($"Erro ao enviar e-mail: {response.StatusCode} - {body}");
            }
        }
    }
}
