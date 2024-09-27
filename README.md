
# vCard.Net

vCard.Net is a .NET library designed to create, parse, and manipulate vCard files. vCards are electronic business cards that are widely used for sharing contact information.

## Features

- **vCard Parsing**: Easily parse vCard files and extract contact information.
- **vCard Generation**: Generate vCard files with custom contact information.
- **vCard Properties**: Support for a wide range of vCard properties like name, phone number, email, address, and more.
- **Serialization/Deserialization**: Serialize vCard objects to text and deserialize vCard files into objects.
- **Cross-Platform**: Works on .NET Core and .NET Framework.

## Installation

To use `vCard.Net` in your project, install it via NuGet:

```bash
dotnet add package vCard.Net
```

Alternatively, you can clone this repository and build the project locally:

```bash
git clone https://github.com/gachris/vCard.Net.git
cd vCard.Net
dotnet build
```

## Usage

Here’s a basic example to get you started with using `vCard.Net`:

### Parsing a vCard file

```csharp
using System;
using System.IO;
using System.Linq;
using vCard.Net.Serialization;

// Read vCard data from a file
var filePath = "path/to/vCard_v21.vcf";
var vCardData = File.ReadAllText(filePath);

// Create a stream from the vCard data
using var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(vCardData));
using var reader = new StreamReader(memoryStream);

// Deserialize the vCard
var vCard = (CardComponents.vCard)SimpleDeserializer.Default.Deserialize(reader).First();

// Access some properties
Console.WriteLine(vCard.FormattedName);
Console.WriteLine(vCard.Emails.FirstOrDefault()?.Value);
```

### Creating a vCard

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;

// Create a vCard
var vCard = new CardComponents.vCard
{
    Version = vCardVersion.vCard2_1,
    FormattedName = "John Doe",
    N = new StructuredName { GivenName = "John", FamilyName = "Doe" },
    Emails = new List<Email> 
    { 
        new Email 
        { 
            Value = "john.doe@example.com", 
            PreferredOrder = 1, 
            Types = new List<string> { "INTERNET" } 
        }
    }
};

// Serialize the vCard to a string
var serializer = new ComponentSerializer();
var vCardAsString = serializer.SerializeToString(vCard);

// Save the vCard to a file
var outputPath = "path/to/new_vcard.vcf";
File.WriteAllText(outputPath, vCardAsString);

// Output
Console.WriteLine("vCard saved to: " + outputPath);
```

## Project Structure

- **vCardComponents**: Core logic related to vCard properties and data types.
- **Collections**: Data structures for managing vCard properties.
- **DataTypes**: Defines the various vCard fields (e.g., Address, Email, Phone, etc.).
- **Serialization**: Serialization and deserialization logic for vCard files.

## Contributing

Contributions are welcome! Please follow these steps if you wish to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Write clean, readable code and add comments where necessary.
4. Submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
