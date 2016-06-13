package gisshpreader.lunarmonk.com.shprreader;

import android.content.Context;
import android.content.res.AssetManager;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import org.apache.commons.io.IOUtils;

import java.io.File;
import java.io.InputStream;
import java.io.OutputStream;

import diewald_shapeFile.files.dbf.DBF_Field;
import diewald_shapeFile.files.dbf.DBF_File;
import diewald_shapeFile.files.shp.SHP_File;
import diewald_shapeFile.files.shp.shapeTypes.ShpPolygon;
import diewald_shapeFile.files.shp.shapeTypes.ShpShape;
import diewald_shapeFile.files.shx.SHX_File;
import diewald_shapeFile.shapeFile.ShapeFile;

public class ShprReaderActivity extends AppCompatActivity {



    @Override
    protected void onCreate(Bundle savedInstanceState) {


        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        initReader();
    }

    private void initReader()
    {
        DBF_File.LOG_INFO           = !false;
        DBF_File.LOG_ONLOAD_HEADER  = false;
        DBF_File.LOG_ONLOAD_CONTENT = false;

        SHX_File.LOG_INFO           = !false;
        SHX_File.LOG_ONLOAD_HEADER  = false;
        SHX_File.LOG_ONLOAD_CONTENT = false;

        SHP_File.LOG_INFO           = !false;
        SHP_File.LOG_ONLOAD_HEADER  = false;
        SHP_File.LOG_ONLOAD_CONTENT = false;

        try {
            // GET DIRECTORY
            //String curDir = System.getProperty("user.dir");
            //String folder = "/data/Gis Steiermark 2010/Bezirke/BezirkeUTM33N/";
            File file = CopyReadAssets("IND_roads.dbf");
            File file2 = CopyReadAssets("IND_roads.shp");
            File file3 = CopyReadAssets("IND_roads.shx");
            String path = file.getPath();
            String absPath = file.getAbsolutePath();
            path = path.substring(0, path.indexOf("IND_roads.dbf"));
            // LOAD SHAPE FILE (.shp, .shx, .dbf)
            ShapeFile shapefile = new ShapeFile(path, "IND_roads").READ();

            // TEST: printing some content
            ShpShape.Type shape_type = shapefile.getSHP_shapeType();
            System.out.println("\nshape_type = " +shape_type);

            int number_of_shapes = shapefile.getSHP_shapeCount();
            int number_of_fields = shapefile.getDBF_fieldCount();

            for(int i = 0; i < number_of_shapes; i++){
                ShpPolygon shape    = shapefile.getSHP_shape(i);
                String[] shape_info = shapefile.getDBF_record(i);

                ShpShape.Type type     = shape.getShapeType();
                int number_of_vertices = shape.getNumberOfPoints();
                int number_of_polygons = shape.getNumberOfParts();
                int record_number      = shape.getRecordNumber();

                System.out.printf("\nSHAPE[%2d] - %s\n", i, type);
                System.out.printf("  (shape-info) record_number = %3d; vertices = %6d; polygons = %2d\n", record_number, number_of_vertices, number_of_polygons);

                for(int j = 0; j < number_of_fields; j++){
                    String data = shape_info[j].trim();
                    DBF_Field field = shapefile.getDBF_field(j);
                    String field_name = field.getName();
                    System.out.printf("  (dbase-info) [%d] %s = %s", j, field_name, data);
                }
                System.out.printf("\n");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private File CopyReadAssets(String filename) {
        AssetManager assetManager = getAssets();

        InputStream in = null;
        OutputStream out = null;
        File file = new File(getFilesDir(), filename);
        try {
            in = assetManager.open(filename);
            out = openFileOutput(file.getName(), Context.MODE_WORLD_READABLE);

            IOUtils.copy(in, out);

            in.close();
            in = null;
            out.flush();
            out.close();
            out = null;
        } catch (Exception e) {
            Log.e("tag", e.getMessage());
        }

        return file;


    }
}
