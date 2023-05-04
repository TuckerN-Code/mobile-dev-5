package com.delware.layouts;

import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Toast;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.linear_layout);

		//create a file in internal storage
        if (!createFileInInternalStorage(this, "temp.txt", "Hello World!"))
            Toast.makeText(this, "Failed to save file to internal storage", Toast.LENGTH_SHORT).show();

		//retrieve that file from internal storage
        File internalFile = retrieveFileFromInternalStorage("temp.txt");

		//create a toast from that file
        createToastFromFileContent(internalFile);
		
		deleteFileFromInternalStorage("temp.txt");
    }

    private boolean createFileInInternalStorage(Context context, String fileName, String fileContent)
    {
        //getFilesDir() gets you the path to internal storage on the device
        //Below are a few cool things about internal storage
        //1. It's always available.
        //2. Files saved here are accessible by only your app.
        //3. When the user uninstalls your app, the system removes all your app's files from internal storage.
        //4. Internal storage is best when you want to be sure that neither the user nor other apps can access your files.

		//this line creates a file in internal storage
        File file = new File(getFilesDir()+"/"+fileName);

		//write the content to the file
        try {
            FileWriter fileWriter = new FileWriter(file);
            fileWriter.append(fileContent);
            fileWriter.flush();
            fileWriter.close();
        } catch (IOException e) {
            e.printStackTrace();
            return false;
        }	
		
		//returns true on success, false on failure
        return true;
    }

    private File retrieveFileFromInternalStorage(String fileName)
    {
		//returns the file from internal storage
        return new File(getFilesDir()+"/"+fileName);
    }
	
	private boolean deleteFileFromInternalStorage(String fileName)
	{
		File file = retrieveFileFromInternalStorage(fileName);
		
		//returns true if the file was deleted, else false
		return file.delete();
	}

    private void createToastFromFileContent(File internalFile)
    {
        try {
            Scanner in = new Scanner(new FileReader(internalFile));
            StringBuilder sb = new StringBuilder();

            while (in.hasNext())
                sb.append(in.next());

            in.close();

            Toast.makeText(this, sb.toString(), Toast.LENGTH_SHORT).show();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
            Toast.makeText(this, "Failed to read file.", Toast.LENGTH_SHORT).show();
        }
    }
}
