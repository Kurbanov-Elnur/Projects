#include "card.h"
#pragma once

class wallet
{
private:
	std::string ID{};
	uint16_t* securityCode{};
	Balance* balance{};
public:
	uint16_t* cardsCount = new uint16_t{};
	card** cards = new card*[10];
	personalData* ownerData{};
	std::string ownerEmail{};
	std::string ownerPhone{};
	std::string currency{};
	uint16_t* dailySpendingLimit{};
	Transaction** Transactions = new Transaction*[50]{};
	uint16_t* tranasctionCount = new uint16_t{};

	wallet(personalData&, std::string&, std::string&, std::string&, std::string&, uint16_t&, Balance&);

	friend std::ostream& operator << (std::ostream& os, const wallet _wallet)
	{
		if (typeid(os) == typeid(std::ofstream))
		{
			os
				<< *_wallet.ownerData
				<< _wallet.ownerEmail << std::endl
				<< _wallet.ownerPhone << std::endl
				<< _wallet.ID << std::endl
				<< *_wallet.securityCode << std::endl
				<< _wallet.currency << std::endl
				<< *_wallet.balance << std::endl;
			for (size_t i = 0; i < *_wallet.cardsCount; i++)
			{
				os << _wallet.cards[i]->getCardNumber();
			}return os;
		}
		os
			<< *_wallet.ownerData
			<< "Owner email: " << _wallet.ownerEmail << std::endl
			<< "Owner phone: " << _wallet.ownerPhone << std::endl
			<< "Wallet ID: " << _wallet.ID << std::endl
			<< "Wallet security code: " << *_wallet.securityCode << std::endl
			<< "Wallet currency: " << _wallet.currency << std::endl
			<< "Balance: " <<  * _wallet.balance 
			<< "Cards in wallet: " << std::endl;
		for (size_t i = 0; i < *_wallet.cardsCount; i++)
		{
			os << "Card number: " << _wallet.cards[i]->getCardNumber();
		}
		return os;
	}

	friend std::istream& operator>>(std::istream& is, wallet& _wallet)
	{
		is >> *_wallet.ownerData >> _wallet.ownerPhone >> _wallet.ID >> *_wallet.securityCode >> _wallet.currency >> *_wallet.balance;
	}

	void addCard();
	void cardReplenishment();
	void walletReplenishment();

	Balance getBalance() const;
	std::string getID() const;
	uint16_t getSecurityCode() const;
};