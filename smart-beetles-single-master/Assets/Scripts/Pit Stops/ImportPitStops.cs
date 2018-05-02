using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class ImportPitStops : MonoBehaviour
{

	public static ImportPitStops instance;
	public PitStops pitStops;

	private void Awake ()
	{

		if (instance == null) {
			
			instance = this;

		} else if (instance != this) {

			Destroy (gameObject);

		}

		DontDestroyOnLoad (gameObject);

	}

	private IEnumerator Start ()
	{
		
		// WWW www = new WWW ("http://gis1.sandag.org/sdgis/rest/services/Transportation/BTW_PitStops_2017/MapServer/0/query?where=1%3D1&text=&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&relationParam=&outFields=&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&returnIdsOnly=false&returnCountOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&gdbVersion=&returnDistinctValues=false&f=pjson");

		WWW www = new WWW ("http://geotec.init.uji.es/arcgis/rest/services/Hosted/BikePitStops/FeatureServer/0/query?where=1%3D1&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=false&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&sqlFormat=none&resultType=&f=pjson");
		yield return www;
		JsonData data = JsonMapper.ToObject (www.text);

		/*
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
		*/

		pitStops.exceededTransferLimit = data ["exceededTransferLimit"].ToString () == "true";

		for (int i = 0; i < data ["features"].Count; i++) {

			pitStops.features.Add (new Feature () { 

				attributes = new Attributes () { 

					name = data ["features"] [i] ["attributes"] ["name"].ToString ()

				}, 

				geometry = new Geometry () { 

					x = double.Parse (data ["features"] [i] ["geometry"] ["x"].ToString ()), 
					y = double.Parse (data ["features"] [i] ["geometry"] ["y"].ToString ()) 

				} 

			});

		}

		for (int i = 0; i < data ["fields"].Count; i++) {

			pitStops.fields.Add (new Field () {

				name = data ["fields"] [i] ["name"].ToString (),
				type = data ["fields"] [i] ["type"].ToString (),
				alias = data ["fields"] [i] ["alias"].ToString (),
				length = int.Parse (data ["fields"] [i] ["length"].ToString ())

			});

		}

		pitStops.geometryType = data ["geometryType"].ToString ();
		pitStops.spatialReference.wkid = int.Parse (data ["spatialReference"] ["wkid"].ToString ());
		pitStops.spatialReference.latestWkid = int.Parse (data ["spatialReference"] ["latestWkid"].ToString ());
		pitStops.globalIdFieldName = data ["globalIdFieldName"].ToString ();
		pitStops.objectIdFieldName = data ["objectIdFieldName"].ToString ();
		pitStops.hasZ = data ["hasZ"].ToString () == "true";
		pitStops.hasM = data ["hasM"].ToString () == "true";

	}

	public string GetName (int i)
	{

		if (pitStops.features.Count > 0) {
			
			return pitStops.features [i].attributes.name;

		} else {

			return "error";

		}

	}

	public Vector3 GetXY (int i)
	{

		if (pitStops.features.Count > 0) {

			string stringX = (pitStops.features [i].geometry.x * 100).ToString ().Remove (0, 5);
			string stringY = (pitStops.features [i].geometry.y * 100).ToString ().Remove (0, 3);

			// A ver, los números que aparecen a continuación dan mucho miedo y parecen que sean
			// números mágicos que están ahí para que el juego funcione, pero la verdad es que
			// tienen su lógica y cálculo matemático hecho en papel (simplemente te lo tendrás
			// que creer y no tocar nada si no quieres que se vaya todo a la m*****). El primer
			// número (7.6686 en el caso de las X y 0.2487 en el de las Y) es como la coordenada
			// (0, 0) en el mapa de ArcGIS (habiendo quitado los primeros dígitos, ya que son
			// innecesarios para un mapa tan pequeño). Al restar este número con el obtenido
			// justo antes de este párrafo que ya te estás cansando de leer se obtiene la distancia
			// que hay desde el comienzo del mapa hasta el punto que queremos obtener. Después,
			// se hace una regla de 3 con otros dos datos. El primero es el tamaño del mapa del
			// juego, 400 en las X y 300 en las Y. El otro número que parece "mágico" (2.1072 en
			// las X y 1.3379 en las Y) es el tamaño del mapa en ArcGIS (quitando otra vez los
			// primeros dígitos). Al resultado de la regla de 3 se le resta la mitad del tamaño
			// del mapa del juego (ya que el origen (0, 0) está en el centro) y se obtiene lo
			// que viene siendo la posX y posY. Bueno, espero que se haya entendido, feliz día.

			float posX = (7.6686f - float.Parse (stringX)) * 400 / 2.1072f - 200;
			float posY = (float.Parse (stringY) - 0.2487f) * 300 / 1.3379f - 150;

			return new Vector3 (posX, posY, 0);

		} else {

			return new Vector3 (0, 0, 0);

		}

	}

	public int GetID (string s)
	{

		if (pitStops.features.Count > 0) {
		
			for (int i = 0; i < pitStops.features.Count; i++) {
			
				if (pitStops.features [i].attributes.name == s) {
				
					return i;
				
				}
			
			}

			return -1;
		
		} else {
		
			return -1;
		
		}

	}

}
