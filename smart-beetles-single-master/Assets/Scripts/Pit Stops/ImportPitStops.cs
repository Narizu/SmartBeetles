using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class ImportPitStops : MonoBehaviour
{

	public static ImportPitStops instance;
	public PitStops pitStops;

	void Awake ()
	{

		if (instance == null) {
			
			instance = this;

		} else if (instance != this) {

			Destroy (gameObject);

		}

		DontDestroyOnLoad (gameObject);

	}

	IEnumerator Start ()
	{
		
		WWW www = new WWW ("http://gis1.sandag.org/sdgis/rest/services/Transportation/BTW_PitStops_2017/MapServer/0/query?where=1%3D1&text=&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&relationParam=&outFields=&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&returnIdsOnly=false&returnCountOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&gdbVersion=&returnDistinctValues=false&f=pjson");
		yield return www;
		JsonData data = JsonMapper.ToObject (www.text);

		pitStops.displayFieldName = data ["displayFieldName"].ToString ();
		pitStops.fieldAliases.Name = data ["fieldAliases"] ["Name"].ToString ();
		pitStops.geometryType = data ["geometryType"].ToString ();
		pitStops.spatialReference.wkid = int.Parse (data ["spatialReference"] ["wkid"].ToString ());
		pitStops.spatialReference.latestWkid = int.Parse (data ["spatialReference"] ["latestWkid"].ToString ());

		for (int i = 0; i < data ["fields"].Count; i++) {
			
			pitStops.fields.Add (new Field () {
				
				name = data ["fields"] [i] ["name"].ToString (),
				type = data ["fields"] [i] ["type"].ToString (),
				alias = data ["fields"] [i] ["alias"].ToString (),
				length = int.Parse (data ["fields"] [i] ["length"].ToString ())
					
			});

		}

		for (int i = 0; i < data ["features"].Count; i++) {
			
			pitStops.features.Add (new Feature () { 
				
				attributes = new Attributes () { 
					
					Name = data ["features"] [i] ["attributes"] ["Name"].ToString ()
						
				}, 

				geometry = new Geometry () { 
					
					x = double.Parse (data ["features"] [i] ["geometry"] ["x"].ToString ()), 
					y = double.Parse (data ["features"] [i] ["geometry"] ["y"].ToString ()) 
						
				} 
					
			});

		}

		print (pitStops.features [0].geometry.x);

	}

	public string GetName (int i)
	{

		if (pitStops.features.Count > 0) {
			
			return pitStops.features [i].attributes.Name;

		} else {

			return "error";

		}

	}
	/*
	public Vector3 GetXY (int i)
	{

		return new Vector3 (float.Parse (pitStops.features [i].geometry.x), 0.0f, float.Parse (pitStops.features [i].geometry.y));

	}
*/
}
