﻿using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class Query<TPayload, TResult> : IAsyncQuery<TResult>
			where TPayload : IAggregateRoot
	{
		protected readonly ILogger logger;
		private readonly IDataContext storageContext;
		private readonly DatabaseParts parts;

		public Query(IDataContext storageContext, ILoggerFactory loggerFactory, DatabaseParts parts)
		{
			this.storageContext = storageContext;
			this.logger = loggerFactory.CreateLogger(GetType());
			this.parts = parts;
		}

		public void Dispose()
		{
		}

		public abstract Task<TResult> Execute();

		protected IDataReader<TPayload> Read() => this.storageContext.GetReader<TPayload>(this.parts.Parts);
	}
}