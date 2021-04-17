using ConclusaoDeAtividade.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ConclusaoDeAtividade.Controllers
{
    [ApiController]
    [Route("conclusao")]
    public class ConclusaoController : ControllerBase
    {
        private readonly string CHANNEL_NAME_CORRECAO = "correcao";
        private static Contador _contador = new Contador();

        public ConclusaoController()
        {
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new
            {
                QtdAtividadeRespondida = _contador.ValorAtual
            });
        }


        [HttpPost]
        public IActionResult ResponderAtividade(
            [FromBody] FinalizacaoDeAtividade finalizacaoDeAtividadeDto,
            [FromServices] RabbitMQConfigurations configurations)
        {
            lock (_contador)
            {
                _contador.Incrementar();

                var factory = new ConnectionFactory()
                {
                    HostName = configurations.HostName,
                    Port = configurations.Port,
                    UserName = configurations.UserName,
                    Password = configurations.Password
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: CHANNEL_NAME_CORRECAO, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var bodyString = JsonSerializer.Serialize(finalizacaoDeAtividadeDto);
                    var body = Encoding.UTF8.GetBytes(bodyString);

                    channel.BasicPublish(
                        exchange: "", routingKey: CHANNEL_NAME_CORRECAO, basicProperties: null, body: body);
                }

                return Ok(new
                {
                    Resultado = $"Mensagem encaminhada com sucesso"
                });
            }
        }
    }
}
