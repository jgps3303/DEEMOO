using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;

public class MyEditor : MonoBehaviour
{
    [MenuItem("mytools/excelToTxt")]
    public static void ExportExcelTotext()
    {
        //EXCEL路徑
        string assetPath = Application.dataPath+"/_Excel";
        //獲得文件
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");
        for (int i = 0; i < files.Length; i++)
        {
            //替換斜線
            files[i] = files[i].Replace('\\','/');
            //讀取
            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                //文件流轉換成EXCEL
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);

                //獲得數據
                DataSet dataSet = excelDataReader.AsDataSet();

                //讀取第一章表
                DataTable table = dataSet.Tables[0];

                //讀取後存到對應的txt中
                readTableToTxt(files[i], table);
            }
        }

        AssetDatabase.Refresh();
    }

    private static void readTableToTxt(string filePath, DataTable table)
    {
        //獲得文件名 並生成對應的txt
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string path = Application.dataPath+"/Resources/Data/" + fileName + ".txt";

        //判斷是否已經存在 市則刪除
        if(File.Exists(path))
        {
            File.Delete(path);
        }
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];

                    string str = "";
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = dataRow[col].ToString();

                        str = str + val + "\t";
                    }

                    sw.Write(str);

                    if(row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
