using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideSpriteAnimationsTexture : MonoBehaviour {
	private SpriteRenderer sr;

	private static int idMainTex = Shader.PropertyToID("_MainTex");
	private static MaterialPropertyBlock block;

	[SerializeField] 
	private Texture texture = null;
	public Texture overrideTexture{
		get{ return texture; }
		set{ texture = value; }
	}

	void Awake()
	{
		if( block == null)
			block = new MaterialPropertyBlock ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void LateUpdate()
	{
		block.SetTexture (idMainTex, texture);
		sr.SetPropertyBlock (block);
	}
}
