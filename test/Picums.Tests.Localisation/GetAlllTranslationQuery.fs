module Picums.Tests.Localisation

    open System
    open Picums.Localisation.Data
    open Picums.Data.InMemory
    open Picums.Data.CQRS.DataAccess
    open Picums.Tests
    open Should.Fluent
    open Xunit 

    module GetAllTranslationQuery =
        let context = InMemoryDataContext()
        let logger = TestLoggerFactory()

        [<Fact>]
        let ``GetAllTranslationsQuery returns QueryForAll`` () =
            let query = new GetAllTranslationsQuery(context,logger)
            let result = query.GetQuery(DatabaseParts("test", "test"))

            result.Should().Not.Be.Null()