import 'dart:developer';

class ApiService {
  ApiService();

  Iterable<String> pageFiles(int counter, int size) {
    var folders = <String>[];

    for (var i = counter; i < (counter + size); i++) {
      folders.add('Hello $i');
    }

    return folders;
  }
}