import 'dart:convert';
import 'package:http/http.dart' as http;

class ApiService {
  final baseUrl = 'https://localhost:49170';

  ApiService();

  Future<List<String>> listFiles() async {
    var url = Uri.parse('$baseUrl/files');

    final response = await http.get(
      url,
      headers: {'Access-Control-Allow-Origin': 'true'},
    );

    Iterable body = json.decode(response.body);

    return body.map((e) => e.toString()).toList();
  }
}
