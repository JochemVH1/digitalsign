using System;
using System.Collections.Generic;
using System.Linq;

namespace digitalsign.application.Sensor.Core
{
    public class RegistrationManager
    {
        private readonly HashSet<RegistrationResult> _registrations;
        
        private static RegistrationManager _registrationManager;

        private RegistrationManager()
        {
            _registrations = new HashSet<RegistrationResult>();
        }

        public static RegistrationManager Instance()
        {
            return _registrationManager ??= new RegistrationManager();
        }

        public RegistrationResult Validate(string token)
        {
            try
            {
                var result = _registrations.Single(x => x.Token.Equals(token));
                if (!_registrations.TryGetValue(result, out var registrationResult)) return RegistrationResultFactory.Invalid(token);
                registrationResult.State = ValidationState.Validated;
                _registrations.Remove(result);
                return _registrations.Add(registrationResult) ? registrationResult : RegistrationResultFactory.Invalid(token);
            }
            catch (Exception)
            {
                return RegistrationResultFactory.Invalid(token);
            }
        }
        public RegistrationResult? Register()
        {
            return Register(Guid.NewGuid().ToString());
        }
        public RegistrationResult? Register(string token)
        {
            var result = RegistrationResultFactory.New(token, ValidationState.Registered);
            if (_registrations.Add(result))
            {
                return result;
            }
            return null;
        }
    }

    public struct RegistrationResult
    {
        public string Token;
        public DateTime CreationDate => DateTime.Now;

        public ValidationState State { get; set; }
    }

    internal class RegistrationResultFactory
    {
        public static RegistrationResult Invalid(string token) => new RegistrationResult
            {State = ValidationState.Invalid, Token = token};
        public static RegistrationResult New(string token, ValidationState state) => new RegistrationResult
            { State = state, Token = token };

    }

    public enum ValidationState {
        Registered,
        Validated,
        Invalid
    }
}
