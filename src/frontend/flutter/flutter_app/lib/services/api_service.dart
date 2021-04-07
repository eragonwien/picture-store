import 'dart:convert';
import 'package:flutter_app/env.dart';
import 'package:http/http.dart' as http;

class ApiService {
  ApiService();

  Future<List<String>> listFiles() async {
    var uri = Uri.parse('$appBaseUrl/files');

    final response = await http.get(
      uri,
      headers: {'Access-Control-Allow-Origin': 'true'},
    );

    var rawBody = jsonDecode(response.body) as List;

    return rawBody.map((item) => item as String).toList();
  }

  Future<Map<String, List<String>>> pageFiles() async {
    var uri = Uri.parse('$appBaseUrl/files/page');

    final response = await http.get(
      uri,
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

  Future deleteFile(String imageSource) async {
    await http.delete(
      Uri.parse(imageSource),
      headers: {'Access-Control-Allow-Origin': 'true'},
    );
  }

  Future uploadAsync(String path) async {
    var uri = Uri.parse('$appBaseUrl/files');

    var request = http.MultipartRequest('POST', uri);
    request.files.add(await http.MultipartFile.fromPath('file', path));

    await request.send();
  }
}
