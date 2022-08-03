using Microsoft.EntityFrameworkCore.Query;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace Ensek.EnergyManager.Tests.UnitTests;
// taken from https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking  
class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
	public TestAsyncEnumerable(IEnumerable<T> enumerable)
		: base(enumerable)
	{ }

	public TestAsyncEnumerable(Expression expression)
		: base(expression)
	{ }

	IQueryProvider IQueryable.Provider
	{
		get { return new TestAsyncQueryProvider<T>(this); }
	}

	public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
	{
		return GetEnumerator();
	}

	public IAsyncEnumerator<T> GetEnumerator()
	{
		return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
	}
}

internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
	private readonly IEnumerator<T> _inner;

	public TestAsyncEnumerator(IEnumerator<T> inner)
	{
		_inner = inner;
	}

	public T Current
	{
		get
		{
			return _inner.Current;
		}
	}

	public void Dispose()
	{
		_inner.Dispose();
	}

	public ValueTask DisposeAsync()
	{
		Dispose();
		return ValueTask.CompletedTask;
	}

	public Task<bool> MoveNext(CancellationToken cancellationToken)
	{
		return Task.FromResult(_inner.MoveNext());
	}

	public ValueTask<bool> MoveNextAsync()
	{
		return ValueTask.FromResult(_inner.MoveNext());
	}
}

internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
	private readonly IQueryProvider _inner;

	internal TestAsyncQueryProvider(IQueryProvider inner)
	{
		_inner = inner;
	}

	//public IQueryable CreateQuery(Expression expression)
	//{
	//	return new TestAsyncEnumerable<TEntity>(expression);
	//}

	//public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
	//{
	//	return new TestAsyncEnumerable<TElement>(expression);
	//}

	public IQueryable CreateQuery(Expression expression)
	{
		switch (expression)
		{
			case MethodCallExpression m:
				{
					var resultType = m.Method.ReturnType; // it shoud be IQueryable<T>
					var tElement = resultType.GetGenericArguments()[0];
					var queryType = typeof(TestAsyncEnumerable<>).MakeGenericType(tElement);
					return (IQueryable)Activator.CreateInstance(queryType, expression);
				}
		}
		return new TestAsyncEnumerable<TEntity>(expression);
	}

	public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
	{
		var queryType = typeof(TestAsyncEnumerable<>).MakeGenericType(typeof(TElement));
		return (IQueryable<TElement>)Activator.CreateInstance(queryType, expression);
	}

	public object Execute(Expression expression)
	{
		return _inner.Execute(expression);
	}

	public TResult Execute<TResult>(Expression expression)
	{
		return _inner.Execute<TResult>(expression);
	}

	public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
	{
		return new TestAsyncEnumerable<TResult>(expression);
	}

	public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
	{
		return Task.FromResult(Execute<TResult>(expression));
	}

	TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
	{
		return Execute<TResult>(expression);
	}
}
