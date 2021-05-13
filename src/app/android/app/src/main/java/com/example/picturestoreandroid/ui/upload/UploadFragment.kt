package com.example.picturestoreandroid.ui.upload

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import android.provider.MediaStore
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.fragment.app.Fragment
import com.example.picturestoreandroid.R
import com.example.picturestoreandroid.services.FileService

class UploadFragment : Fragment(), View.OnClickListener {

    private val pickImageRequestCode = 100;
    private val fileService = FileService(this);

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val root = inflater?.inflate(R.layout.fragment_upload, container, false)

        val uploadButton: Button = root.findViewById(R.id.upload_button)
        uploadButton.setOnClickListener(this);

        return root
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (resultCode != Activity.RESULT_OK) return

        when (requestCode) {
            pickImageRequestCode -> onImagePicked(data)
            else -> Log.d("Debug", "No action for code $requestCode found")
        }
    }

    private fun onImagePicked(data: Intent?) {

        for (i in 0..data?.clipData!!.itemCount) {
            val clipData = data?.clipData!!.getItemAt(i)
            Log.d("Debug", "Uri: ${clipData.uri} is added to background queue")
        }
    }

    override fun onClick(view: View?) {
        if(view == null) return;

        when (view.id) {
            R.id.upload_button -> onUploadButtonClicked()
            else -> Log.d("Debug", "Any Clicked")
        }
    }

    private fun onUploadButtonClicked() {
        Log.d("Debug", "Upload Button clicked")
        fileService.openGalleryForImage(pickImageRequestCode)
    }
}