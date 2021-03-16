package snguyen.picturestore.picturestoreapp

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.fragment.app.Fragment

/**
 * A simple [Fragment] subclass as the second destination in the navigation.
 */
class SecondFragment : Fragment() {

    private var apiService: ApiService = ApiService()

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_second, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        view.findViewById<Button>(R.id.button_second).setOnClickListener {

            apiService.requestData(context);

//            // Request a string response from the provided URL.
//            val stringRequest = StringRequest(
//                Request.Method.GET,
//                url,
//                Response.Listener<String> { response ->
//                    // Display the first 500 characters of the response string.
//                    Toast.makeText(context, "Response is: ${response.toString()}", Toast.LENGTH_SHORT).show()
//                },
//                Response.ErrorListener {
//                    Toast.makeText(context, "Error occurred", Toast.LENGTH_SHORT).show()
//                })
//
//            // Add the request to the RequestQueue.
//            queue.add(stringRequest)

//            findNavController().navigate(R.id.action_SecondFragment_to_FirstFragment)
        }
    }
}