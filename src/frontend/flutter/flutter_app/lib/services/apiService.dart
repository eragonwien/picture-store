import 'dart:convert';

import 'package:flutter_app/models/ImageFolderMap.dart';
import 'package:http/http.dart' as http;

class ApiService {
  final baseUrl = 'https://localhost:49170';

  ApiService();

  Future<Iterable<String>> pageFiles(String start, int size) async {
    var url = Uri.parse('$baseUrl/files/page?length=$size&start=$start');

    final response = await http.get(
      url,
      headers: {'Access-Control-Allow-Origin': 'true'},
    );

    var list = json.decode(response.body) as Map<String, dynamic>;

    var results = list.entries.map((e) => e.key).toList();

    return results;
  }
}
