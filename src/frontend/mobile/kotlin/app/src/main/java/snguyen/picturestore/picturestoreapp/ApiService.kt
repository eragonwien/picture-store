package snguyen.picturestore.picturestoreapp

import android.content.Context
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley

class ApiService {
    fun requestData(context: Context?){
        val queue = Volley.newRequestQueue(context)
        val url = "https://jsonplaceholder.typicode.com/todos/1"

        // Request a string response from the provided URL.
        val stringRequest = StringRequest(
            Request.Method.GET,
            url,
            Response.Listener<String> { response ->
                // Display the first 500 characters of the response string.
                println(response.toString())
            },
            Response.ErrorListener { println("That didn't work!") })

        // Add the request to the RequestQueue.
        queue.add(stringRequest)
    }
}