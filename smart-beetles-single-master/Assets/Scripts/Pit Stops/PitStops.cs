using System;
using System.Collections.Generic;

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
