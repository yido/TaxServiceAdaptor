
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaxServiceAdaptor.DTO;
using ValidationException = TaxServiceAdaptor.DTO.Exceptions.ValidationException;

namespace TaxServiceAdaptor
{
    public static class CommandExtensions
    {
        //~ If there is any validation error on your request Object, Custom Exceptions will be thrown ~//
        public static bool Validate<T>(this Command<Request, T> cmd) where T : Response
        { 

            var validator = new DataAnnotationsValidator ();
            var validationResults = new List<ValidationResult> ();
            var isValid = validator.TryValidateObjectRecursive(cmd.Payload, validationResults);
 
            if(!isValid){
                throw new ValidationException(validationResults);
            }
            return isValid;
        } 
    }
}