using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapReader : MonoBehaviour {

    //データソース
	public List<string[]> csvDatas = new List<string[]>();
    public int [,] mapData;

    bool isAlreadyRead = false;

    public　MapReader() {}
    public　MapReader(string fileName) {
        this.read(fileName);
    }

	public void read(string fileName) {
		// csvをロード
		TextAsset csv = Resources.Load(fileName) as TextAsset;
		StringReader reader = new StringReader (csv.text);
		while (reader.Peek () > -1) {
			// ','ごとに区切って配列へ格納
			string line = reader.ReadLine();
			csvDatas.Add(line.Split(','));
		}
        this.isAlreadyRead = true;
        this.convert();
    }
    void convert() {
        this.mapData = new int[this.csvDatas.Count, this.csvDatas[0].Length - 1];
        for (int y = 0; y < this.csvDatas.Count; y++) {
            for (int x = 0; x < this.csvDatas[y].Length - 1; x++) {
                this.mapData[x, y] = int.Parse(this.csvDatas[y][x]);
            }
        }
    }

    //要素の交換
    public void swap(int x1, int y1, int x2, int y2) {
        if (!this.isAlreadyRead) return;

        int tmp = this.mapData[x1, y1];
        this.mapData[x1, y1] = this.mapData[x2, y2];
        this.mapData[x2, y2] = tmp;
    }
}