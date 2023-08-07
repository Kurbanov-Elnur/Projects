#include "wallet.h"
#include <sstream>
#include <string>
#include <regex>
#pragma once

namespace functions
{
	template<typename T>
	void saveInFile(T data, std::string fileName)
	{
		fileName += ".txt";

		std::ofstream file(fileName, std::ios::app);

		if (file.is_open())
			file << data;
		else throw std::invalid_argument("File not found!");

		file.close();
	}

	template <typename T>
	bool loadFromFile(T**& downloadData, std::string fileName, uint16_t& count)
	{
		fileName += ".txt";

		std::ifstream file(fileName);

		T data{};

		if (file.is_open())
		{
			for (size_t i = 0; file >> data; i++)
			{
				downloadData[i] = new T(data);
				count++;
			}
			count--;
		}
		else
		{
			file.close();
			return false;
		}
		file.close();

		return true;
	}

	std::string checkWallets(wallet** _wallet, uint16_t& count);
	void formationOfRatings(wallet wallets);
	void top3cost(wallet wallets);
	void top3category(wallet wallets);
	int getCurrentYear();
	void myCheck(std::string& str, std::regex regexCheck);
	wallet* addWallet();
	Transaction* addTransaction();
	personalData* addPersonalData();
	dateOfExpiry* addDateOfExpiry();
};