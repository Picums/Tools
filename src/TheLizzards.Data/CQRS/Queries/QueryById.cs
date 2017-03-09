﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.Domain;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public sealed class QueryById<TPayload> : IAsyncQuery<Maybe<TPayload>>
		where TPayload : IAggregateRoot
	{
		private readonly IDataContext dataContext;
		private readonly ILoggerFactory loggerFactory;
		private readonly DatabaseParts parts;
		private readonly Guid id;

		public QueryById(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts, Guid id)
		{
			this.dataContext = dataContext;
			this.loggerFactory = loggerFactory;
			this.parts = parts;
			this.id = id;
		}

		public Task<Maybe<TPayload>> Execute()
			=> new QueryByIdBuilder<TPayload>()
				.WithDataContext(this.dataContext)
				.WithLogger(this.loggerFactory)
				.WithDatabaseParts(this.parts)
				.WithId(this.id)
				.Execute();
	}
}