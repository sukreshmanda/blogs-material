#!/usr/bin/env python3
import socket

HOST = '127.0.0.1'
PORT = 9876

with socket.socket(socket. AF_INET, socket. SOCK_STREAM) as s:
    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind((HOST, PORT))
    s.listen()
    print(f'Server listening on {HOST}:{PORT}')
    
    while True:
        conn, addr = s.accept()
        total = 0
        with conn:
            while True: 
                data = conn.recv(65536)
                if not data: 
                    break
                total += len(data)
            print(f'Received {total / 1024 / 1024:.2f} MB')