﻿using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://zsndhvlj:Fqc8-DRxG3KX5tEqqXIDZRoIgsG8HO5C@shark.rmq.cloudamqp.com/zsndhvlj");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);

            channel.QueueDeclare("hello-queue", true, false, false);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {

                string message =$"log {x}";

                var messageBody = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("logs-fanout", "", null, messageBody);

                Console.WriteLine($"Mesajınız Gönderildi : { message }");

            });

          

            Console.ReadLine();
        }
    }
}
