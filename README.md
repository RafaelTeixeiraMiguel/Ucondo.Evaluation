# Ucondo.Evaluation

Este é o projeto para o teste técnico da Ucondo.

# Executar

Para executar o projeto, acesse a raíz do projeto e execute o seguinte comando para buildar a imagem do conteiner:
```bash
  docker build -t ucondo-evaluation .
```

Após a imagem ser construída, utilize o seguinte comando para executar o conteiner:
```bash
  docker run -d -p 8080:8080 ucondo-evaluation
```

Com o conteiner rodando, a seguinte URL será exposta:
- **Swagger:** [http://localhost:8080/swagger](http://localhost:8080/swagger)
