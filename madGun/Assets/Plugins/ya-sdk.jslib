mergeInto(LibraryManager.library, {

  SaveDataExtern: function (data) {
	  
	  if(player != null){
		  	var dataString = UTF8ToString(data);
	
	var json = JSON.parse(dataString);
	
	player.setData(json);
	  }

  },
  
	LoadDataExtern: function () {
		if(player != null){
		player.getData().then(_data => {
		const json = JSON.stringify(_data);
		myGameInstance.SendMessage('GameBootstrap', 'LoadData', json);
	});
		}
  },
  
  SetLeaderboard: function(value){
	  ysdk.getLeaderboards()
  .then(lb => {
    lb.setLeaderboardScore('HighScore', value);
  });
  },
  
  ShowFullScreenAD: function(){
	ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
        },
        onError: function(error) {
        }
    }
	})  
  },
  
    ReviveRewardedAD: function(){
	  ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
		  myGameInstance.SendMessage('YandexAD', 'OpenRewardedAD');
        },
        onRewarded: () => {
        },
        onClose: () => {
		  myGameInstance.SendMessage('YandexAD', 'ReviveRewarded');
		  myGameInstance.SendMessage('YandexAD', 'CloseRewardedAD');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },
  
      DoubleGoldAD: function(){
	  ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
		  myGameInstance.SendMessage('YandexAD', 'OpenRewardedAD');
        },
        onRewarded: () => {
		  myGameInstance.SendMessage('YandexAD', 'GoldRewarded');
        },
        onClose: () => {
		  myGameInstance.SendMessage('YandexAD', 'CloseRewardedAD');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },
  
  GetLanguage: function(){
	var lang = ysdk.environment.i18n.lang;
	
	var bufferSize = lengthBytesUTF8(lang) + 1;
	
	var buffer = _malloc(bufferSize);
	
	stringToUTF8(lang, buffer, bufferSize);
	
	return buffer;
  },

});