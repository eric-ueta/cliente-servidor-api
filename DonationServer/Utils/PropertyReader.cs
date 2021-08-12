using System;

namespace DonationServer.Utils
{
    public static class PropertyReader
    {
        #region Methods

        /// <summary>
        /// Encontra as propriedades do objeto indicado
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetAllParameters<TEntity>(TEntity parameter)
        {
            string parameterString = "";

            try
            {
                var propriedades = parameter.GetType().GetProperties();

                foreach (var prop in propriedades)
                {
                    parameterString += $"{ prop.Name} = { prop.GetValue(parameter) }; ";
                }
            }
            catch { }

            return parameterString;
        }

        #endregion Methods
    }
}