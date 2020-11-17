using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CodeConverterDictionary))]
[CustomPropertyDrawer(typeof(ThemeDictionary))]
[CustomPropertyDrawer(typeof(SizeDictionary))]


public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}


public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
