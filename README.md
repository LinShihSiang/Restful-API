# Restful-API

## 需求說明

使用 Restful API 進行維修單的管理
情境如下，總共有 3 種使用者

- 分店
- 維修廠商
- 總部

### 商店分店
建立維修單(立案)給予總部審核，並在維修完畢確認後後，對維修單進行確認

### 維修廠商
根據維修單內容至分店進行修理，並在維修完畢後更新維修單狀態
若該問題不屬於廠商職責，則會再更新維修單內容，重新分派給對應維修單位

### 總部
更新分派維修單給維修廠商，並在維修完畢後進行最後確認，再將維修單狀態更新(銷案)

## 規則說明

- 已經銷案的維修單不可再進行任何修改
- 要銷案的維修單，需經 3 方確認後，才能進行銷案
