using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.ComponentModel;
using System.Reflection;

public class EnumDescriptionSerializer<TEnum> : SerializerBase<TEnum>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum value)
    {
        var description = GetEnumDescription(value);
        context.Writer.WriteString(description);
    }

    public override TEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var stringValue = context.Reader.ReadString();
        foreach (var enumValue in Enum.GetValues(typeof(TEnum)).Cast<TEnum>())
        {
            if (GetEnumDescription(enumValue) == stringValue)
            {
                return enumValue;
            }
        }

        throw new ArgumentException($"Unknown value: {stringValue}");
    }

    private string GetEnumDescription(TEnum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));

        return attribute == null ? value.ToString() : attribute.Description;
    }
}