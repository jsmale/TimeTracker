using System;

namespace TimeTracker.Domain
{
	public interface IEntity
	{
		Guid Id { get; }
	}
}