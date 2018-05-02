using System;
using System.Collections.Generic;

/*
[Serializable]
public struct FieldAliases
{
	
	public string Name;

}

[Serializable]
public struct SpatialReference
{
	
	public int wkid;
	public int latestWkid;

}

[Serializable]
public struct Field
{
	
	public string name;
	public string type;
	public string alias;
	public int length;

}

[Serializable]
public struct Attributes
{
	
	public string Name;

}

[Serializable]
public struct Geometry
{
	
	public double x;
	public double y;

}

[Serializable]
public struct Feature
{
	
	public Attributes attributes;
	public Geometry geometry;

}

[Serializable]
public struct PitStops
{
	
	public string displayFieldName;
	public FieldAliases fieldAliases;
	public string geometryType;
	public SpatialReference spatialReference;
	public List<Field> fields;
	public List<Feature> features;

}
*/

[Serializable]
public class Attributes
{
	
	public string name;

}

[Serializable]
public class Geometry
{
	
	public double x;
	public double y;

}

[Serializable]
public class Feature
{
	
	public Attributes attributes;
	public Geometry geometry;

}

[Serializable]
public class Field
{
	
	public string name;
	public string type;
	public string alias;
	public int length;

}

[Serializable]
public class SpatialReference
{
	
	public int wkid;
	public int latestWkid;

}

[Serializable]
public class PitStops
{
	
	public bool exceededTransferLimit;
	public List<Feature> features;
	public List<Field> fields;
	public string geometryType;
	public SpatialReference spatialReference;
	public string globalIdFieldName;
	public string objectIdFieldName;
	public bool hasZ;
	public bool hasM;

}
