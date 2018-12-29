# Memo
## 構造
- MGS
  - Status
    - AudioHandler(Audio)
    - DeviceHandler(Input)
    - Info(Info)
    - User(User)
    - Scenes/PlayingScene(Scene)
      - Skin(Skin)
        - PartsDrawing(Skin)
  - Data
    - BMx Files(Chart)
    - Cson/seaurchin Files(Chart)
    - ksh Files (?)(Chart)
    - PMS Files(Chart)
    - UserConfig(UserConfig)

## 詳細
### MGS
- 状態遷移

### AudioHandler
- 音声関連

### DeviceHandler
- 入力関連

### User
- ユーザー設定やリザルト等

### Scenes/PlayingScene
- 全状態
- 子状態の定義をどうするか
#### 渡すクラス
- AudioHandler
- DeviceHandler
- User
- Info
- Timer
#### 関数
- Draw();
  - 描画
- Play();
  - 再生
- Update();
  - 更新

### Skin
- スキン

#### 渡すクラス
- AudioHandler
- DeviceHandler
- User
- Info
- Timer

#### 関数

