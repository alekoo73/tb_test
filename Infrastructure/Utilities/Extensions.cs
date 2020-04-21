using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Infrastructure.Utilities
{
    public static class Extensions
    {
        public static string ParseException(this Exception exception)
        {
            if (exception != null)
            {
                var res = $"→ {exception.Message} → {exception.StackTrace}";
                if (exception is ValidationException)
                {
                    res += ((ValidationException)exception).ValidationResult.ErrorMessage;
                }

                if (exception is DbUpdateException || exception is DbUpdateConcurrencyException)
                {
                    foreach (var entity in ((DbUpdateException)exception).Entries)
                    {
                        res += $"→ {entity.Entity.GetType().Name} in state {entity.State}";

                        if (entity.State != EntityState.Added && entity.OriginalValues != null)
                        {
                            foreach (var property in entity.Properties)
                            {
                                if (property.OriginalValue != null)
                                    res += $"orig→ {property.OriginalValue}";
                                if (property.CurrentValue != null)
                                    res += $"curr→ {property.CurrentValue}";
                            }
                        }


                    }
                }

                if (exception is System.Data.SqlClient.SqlException)
                {
                    if (((System.Data.SqlClient.SqlException)exception).Errors != null)
                    {
                        foreach (System.Data.SqlClient.SqlError error in ((System.Data.SqlClient.SqlException)exception).Errors)
                        {
                            res += $"→ {error.Message}  LineNumber:{error.LineNumber} Number: {error.Number} Server: {error.Server} State:{error.State} Procedure: {error.Procedure} Source: {error.Source}";
                        }
                    }
                }


                return res + ParseException(exception.InnerException);
            }
            else
            {
                return "";
            }
        }

     
    }
}
