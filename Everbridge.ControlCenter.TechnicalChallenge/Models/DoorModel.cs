using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace Everbridge.ControlCenter.TechnicalChallenge.Models
{
    [DataContract]
    public class DoorModel : PostDoorModel
    {
        [DataMember(Name = "id")]
        [Required]
        public string Id { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DoorModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  IsOpen: ").Append(IsOpen).Append("\n");
            sb.Append("  IsLocked: ").Append(IsLocked).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
