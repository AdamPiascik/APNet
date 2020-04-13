using System;
using System.Collections.Generic;

namespace APNet.Core.ErrorHandling
{
    public static class ErrorHandler
    {
        private const ConsoleColor fatalColor = ConsoleColor.Red;
        private const ConsoleColor warningColor = ConsoleColor.Yellow;
        private const ConsoleColor infoColor = ConsoleColor.White;
        private const ConsoleColor replErrorColor = ConsoleColor.Red;

        public static void HandleErrors(
            List<Error> errors)
        {
            bool bFatalErrorsOccurred = false;

            errors.ForEach(
                x =>
                {
                    if (x.Severity == ErrorSeverity.Fatal)
                        bFatalErrorsOccurred = true;

                    HandleError(x);
                }
            );

            if (bFatalErrorsOccurred)
            {
                Console.ForegroundColor = fatalColor;

                Console.WriteLine(
                    $"\nFatal errors occurred. Exiting..."
                );

                Console.ForegroundColor = ConsoleColor.White;

                Environment.Exit(1);
            }
        }

        public static void HandleReplError(
            Error error)
        {
            Console.ForegroundColor =
                error.Severity switch
                {
                    ErrorSeverity.Fatal => fatalColor,
                    ErrorSeverity.Warning => warningColor,
                    ErrorSeverity.Information => infoColor,
                    _ => fatalColor,
                };

            Console.Write(
                $"\n{error.Type} error: {error.Message}\n"
            );

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void HandleError(
            Error error)
        {
            Console.ForegroundColor =
                error.Severity switch
                {
                    ErrorSeverity.Fatal => fatalColor,
                    ErrorSeverity.Warning => warningColor,
                    ErrorSeverity.Information => infoColor,
                    _ => fatalColor,
                };

            string messagePrefix =
                error.Severity switch
                {
                    ErrorSeverity.Fatal => "Fatal error",
                    ErrorSeverity.Warning => "Warning",
                    ErrorSeverity.Information => "Information",
                    _ => "Unknown error",
                };

            Console.Write(
                $"\n{messagePrefix} of type {error.Type}: {error.Message}"
            );

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
