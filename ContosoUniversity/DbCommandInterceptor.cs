using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity
{
    //public class TaggedQueryCommandInterceptorContext : SchoolContext
    //{
    //    private static readonly TaggedQueryCommandInterceptor _interceptor
    //        = new TaggedQueryCommandInterceptor();

    //    public TaggedQueryCommandInterceptorContext(DbContextOptions<SchoolContext> options) : base(options)
    //    {
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //        => optionsBuilder.AddInterceptors(_interceptor);
    //}
    public class TaggedQueryCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            ManipulateCommand(command);

            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            ManipulateCommand(command);

            return new ValueTask<InterceptionResult<DbDataReader>>(result);
        }

        private static void ManipulateCommand(DbCommand command)
        {
            if (command.CommandText.StartsWith("-- Use hint: robust plan", StringComparison.Ordinal))
            {
                command.CommandText += " OPTION (ROBUST PLAN)";
            }
        }
    }
}
