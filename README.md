# 🎮 Unity & C# Game Development Lab

![Engine](https://img.shields.io/badge/Engine-Unity-blue)
![Language](https://img.shields.io/badge/Language-C%23-green)
![Focus](https://img.shields.io/badge/Focus-Gameplay%20Programming%20%26%20Architecture-orange)
![Status](https://img.shields.io/badge/Status-Active%20Learning-brightgreen)

Laboratório de estudos, experimentos e desenvolvimento de jogos focados na **Unity Engine** utilizando **C#**. 

Este repositório consolida implementações práticas de **inteligência artificial**, **sistemas de combate e vida**, **gameplay feel (Juice)**, **física 2D** e **arquitetura orientada a eventos**.

---

## Competências Técnicas & Conceitos Implementados

### 🧠 1. Inteligência Artificial & Máquinas de Estado (FSM)
- **Finite State Machine (FSM)**: Transições limpas entre estados de patrulha (`Patrol`) e perseguição (`Chase`).
- **Lógica de Detecção**: Transição dinâmica baseada em raio de busca e distância vetorial (`Vector2.Distance`).
- **Debugging Visual**: Uso de `OnDrawGizmosSelected` para renderizar zonas de detecção, pontos de patrulha e conexões no *Scene View*.

### ⚔️ 2. Arquitetura de Combate & Sistema de Vida
- **Event-Driven Architecture (C# Events)**: Desacoplamento entre lógica de dados e interface (UI/HUD) usando `Action<int, int>` e `Action`.
- **Overlap Circle Queries**: Detecção precisa de colisão para ataques em área usando `Physics2D.OverlapCircleAll` e `LayerMask`.
- **Mecanismos de Reutilização**: Script de `Health` modular e independente, aplicável tanto a jogadores quanto a inimigos.

### 🏃 3. Advanced Movement & Gameplay Feel ("Juice")
- **Coyote Time**: Permite que o jogador pule alguns milissegundos após sair de uma plataforma, melhorando a responsividade.
- **Jump Buffer**: Armazena a intenção de pulo antes do personagem tocar o chão.
- **Variable Jump Height**: Altura do pulo proporcional ao tempo em que o botão permanece pressionado (`jumpCutMultiplier`).
- **Orientação Dinâmica**: Sincronização do ponto de ataque (`attackPoint`) com a inversão gráfica do personagem (`SpriteRenderer.flipX`).

---

## 📂 Estrutura do Repositório

```text
📁 Unity-GameDev-Lab/
│
├── 📁 Core_Systems/        # Sistemas genéricos (Health, Damage Systems, Event Managers)
├── 📁 Player_Controllers/  # Controllers avançados de movimentação, pulo e combate
├── 📁 AI_Behaviors/        # Máquinas de estado, detecção e comportamento de inimigos
├── 📁 Mechanics_Lab/       # Experimentos isolados de física e matemática vetorial
└── 📄 README.md            # Apresentação do repositório
