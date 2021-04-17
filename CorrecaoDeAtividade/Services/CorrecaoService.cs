using CorrecaoDeAtividade.Models;
using CorrecaoDeAtividade.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace CorrecaoDeAtividade.Services
{
    public class CorrecaoService
    {
        public static bool ConsumoIniciado = false;
        private static readonly string QUEUE_NAME = "correcao";
        private static RepositorioDeAtividadesConcluidas _repositorio = new RepositorioDeAtividadesConcluidas();

        public static void ConsumirAtvidadesConcluidas()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            ConsumoIniciado = true;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: QUEUE_NAME, durable: false, exclusive: false, autoDelete: false, arguments: null);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                consumer.Received += (object model, BasicDeliverEventArgs @event) =>
                {
                    var atividade1 = JsonSerializer.Deserialize<Atividade>(Encoding.UTF8.GetString(@event.Body.Span));
                    Console.WriteLine("msg recebida - " + atividade1);
                    _repositorio.AdicionarAtividade(atividade1);
                };


                channel.BasicConsume(queue: QUEUE_NAME, autoAck: true, consumer: consumer);
            }
        }
    }
}
