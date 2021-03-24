import 'dart:convert';
import 'package:flutter_app/models/ImageFolderModel.dart';
import 'package:http/http.dart' as http;

class ApiService {
  final baseUrl = 'https://localhost:49170';

  ApiService();

  Future<List<FolderModel>> listFiles() async {
    var url = Uri.parse('$baseUrl/files');

    final response = await http.get(
      url,
      headers: {'Access-Control-Allow-Origin': 'true'},
    );

    Iterable ret = json.decode(response.body);

    return List<FolderModel>.from(
        ret.map((data) => FolderModel.fromJson(data)));
  }
}
