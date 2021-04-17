using CorrecaoDeAtividade.Models;
using CorrecaoDeAtividade.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CorrecaoDeAtividade.Services
{
    public class CorrecaoService
    {
        private static readonly string QUEUE_NAME = "correcao";
        private static RepositorioDeAtividadesConcluidas _repositorio;
        public CorrecaoService()
        {
            _repositorio = new RepositorioDeAtividadesConcluidas();
        }

        public static void SetupConsumer()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: QUEUE_NAME, durable: false, exclusive: false, autoDelete: false, arguments: null);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += EventoRecebidoParaConsumo;
                channel.BasicConsume(queue: QUEUE_NAME, autoAck: true, consumer: consumer);
            }
        }

        private static void EventoRecebidoParaConsumo(object sender, BasicDeliverEventArgs @event)
        {
            string message = Encoding.UTF8.GetString(@event.Body.Span);
            var atividade1 = JsonSerializer.Deserialize<Atividade>(message);
            var atividade = new Atividade()
            {
                IdAluno = 0,
                IdAtividade = 0
            };
            _repositorio.AdicionarAtividade(atividade);
        }
    }
}
