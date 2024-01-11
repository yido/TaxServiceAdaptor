using System;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO {
    public class Command<T1, T2> where T1 : Request where T2 : Response {

        public Command (CommandType cmdType, T1 t1) {
            this.CommandType = cmdType;
            this.Payload = t1;
        }
        public CommandType CommandType { get; private set; }
        public T1 Payload { get; private set; }
        public T2 Response { get; set; }

        public Message<ServiceType> ToMessage (ServiceType type) {

            type.DESKey = this.ByteREGCode (type);
            type.PrivateKey = this.PrivateKey (type);
            var msg = new Message<ServiceType> (type);

            msg.Device = this.Device (type);
            msg.Serial = this.SN (type);
            msg.BusId = this.CommandId;
            msg.Content = JsonConvert.SerializeObject (this.Payload, new JsonSerializerSettings { Formatting = Formatting.None });

            return msg;
        }
        private string CommandId {
            get { return (Enum.GetName (typeof (CommandType), this.CommandType)).Split ("__") [1].Replace ("_", "-"); }
        }
        private string Device (ServiceType t) => Is_R_R_01 ? t.Device : t.RegistrationCode;
        private string PrivateKey (ServiceType t) => Is_R_R_01 ? t.PrivateKey : "";
        private string ByteREGCode (ServiceType t) => Is_R_R_01 ? t.DESKey : t.RegistrationCode[ ^ 8..];
        private string SN (ServiceType t) => Is_R_R_01 ? t.Serial : new Random ().Next (0, 1000000).ToString ("D6");

        private bool Is_R_R_01 => this.CommandType != CommandType.PRIVATE_KEY_APPLICATION__R_R_01;
    }
}