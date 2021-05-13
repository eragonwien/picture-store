package com.example.picturestoreandroid.services

import android.content.Intent
import android.provider.MediaStore
import androidx.fragment.app.Fragment

class FileService(val fragment: Fragment) {
    fun openGalleryForImage(requestCode: Int) {
        val gallery = Intent(Intent.ACTION_PICK, MediaStore.Images.Media.INTERNAL_CONTENT_URI)
        gallery.type = "image/*"
        gallery.putExtra(Intent.EXTRA_ALLOW_MULTIPLE, true)

        fragment.startActivityForResult(Intent.createChooser(gallery, "Bilder auswählen"), requestCode)
    }

}