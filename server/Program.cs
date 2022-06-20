﻿using Grpc.Core;
using MagicOnion.Server;

namespace MagicOnionServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //コンソールにログを表示させる
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());

            var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);

            // localhost:12345でListen
            var server = new global::Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort("localhost", 12345, ServerCredentials.Insecure) }
            };

            // MagicOnion起動
            server.Start();

            // コンソールアプリが落ちないようにReadLineで待つ
            Console.ReadLine();
        }
    }
}
