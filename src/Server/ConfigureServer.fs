[<RequireQualifiedAccess>]
module ConfigureServer

open Microsoft.Extensions.DependencyInjection
open GerardSafe.MongoDb.Database.DependencyInjection

let configureServices (services: IServiceCollection) = services.AddMongoDbDatabase()
