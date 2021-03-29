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

    var rawBody = jsonDecode(response.body) as List;

    return rawBody.map((item) => item as String).toList();
  }

  Future<Map<String, List<String>>> pageFiles() async {
    var url = Uri.parse('$baseUrl/files/page');

    final response = await http.get(
      url,
      headers: {'Access-Control-Allow-Origin': 'true'},
    );

    var res = jsonDecode(response.body) as Map<String, dynamic>;

    var results = Map<String, List<String>>();

    res.forEach((key, value) {
      results.putIfAbsent(
          key, () => (value as List).map((e) => e as String).toList());
    });

    return results;
  }
}
