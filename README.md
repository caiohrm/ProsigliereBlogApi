Como executar o projeto
1. Subir com Docker

docker compose down -v
docker compose up --build

2. A API ficará disponível em

http://localhost:8080

3. Health Check
curl http://localhost:8080/health

Resposta esperada:

"OK"

Endpoints da API

Criar post
curl -X POST "http://localhost:8080/api/posts" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Meu primeiro post",
    "content": "Conteúdo criado via curl"
  }'

Listar posts
curl http://localhost:8080/api/posts

Buscar post por ID
curl http://localhost:8080/api/posts/1

Adicionar comentário a um post
curl -X POST "http://localhost:8080/api/posts/1/comments" \
  -H "Content-Type: application/json" \
  -d '{
    "author": "Usuário Teste",
    "content": "Comentário criado via curl"
  }'
