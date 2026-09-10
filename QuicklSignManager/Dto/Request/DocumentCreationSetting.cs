using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class DocumentCreationSetting
{
    /// <summary>
    /// Converted our document into a base_64 value which can be used in this field
    /// </summary>
    [JsonProperty(PropertyName = "base_64_document")]
    public string Base64document { get; set; }

    [JsonProperty(PropertyName = "extract_fields_from_form_fields_with_metadata")]
    public bool ExtractFieldsFromFormFieldsWithMetadata { get; set; }

    /// <summary>
    /// this is used specifically if you're using form field tags in your document
    /// </summary>
    [JsonProperty(PropertyName = "apply_extracted_fields")]
    public bool ApplyExtractedFields { get; set; } = true;

    /// <summary>
    /// this is used specifically if you're using syntax tags in your document
    /// </summary>
    [JsonProperty(PropertyName = "extract_fields_by_tag")]
    public bool ExtractFieldsByTag { get; set; } = true;
}
